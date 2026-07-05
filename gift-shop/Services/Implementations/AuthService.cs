using gift_shop.Authentication;
using gift_shop.Data;
using gift_shop.DTOs.Auth;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;

namespace gift_shop.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _config;

    public AuthService(IAuthRepository authRepository, IConfiguration config)
    {
        _authRepository = authRepository;
        _config = config;
    }

    // ================= LOGIN =================
    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _authRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
            return null;

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
            return null;

        user.LastLogin = DateTime.UtcNow;
        await _authRepository.UpdateUserAsync(user);

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Token = token,
            UserId = user.UserId,
            Email = user.Email,
            Name = $"{user.FirstName} {user.LastName}"
        };
    }

    // ================= REGISTER =================
    public async Task<ApiResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _authRepository.EmailExistsAsync(request.Email))
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Email already exists"
            };
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Status = "Active",
            EmailVerified = false,
            PhoneVerified = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _authRepository.AddUserAsync(user);

        return new ApiResponse
        {
            Success = true,
            Message = "User registered successfully"
        };
    }

    // ================= GOOGLE LOGIN =================
    public async Task<LoginResponse?> GoogleLoginAsync(GoogleLoginRequest request)
    {
        if (string.IsNullOrEmpty(request.IdToken))
            return null;

        GoogleJsonWebSignature.Payload payload;

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
        }
        catch
        {
            return null; // invalid token
        }

        var email = payload.Email;
        var name = payload.Name;

        var user = await _authRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            user = new User
            {
                RoleId = 2,
                FirstName = name ?? "",
                LastName = "",
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()),
                Status = "Active",
                EmailVerified = payload.EmailVerified,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _authRepository.AddUserAsync(user);
        }

        user.LastLogin = DateTime.UtcNow;
        await _authRepository.UpdateUserAsync(user);

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Token = token,
            UserId = user.UserId,
            Email = user.Email,
            Name = $"{user.FirstName} {user.LastName}"
        };
    }

    // ================= PROFILE =================
    public async Task<UserProfileDto?> GetProfileAsync(int userId)
    {
        var user = await _authRepository.GetUserByIdAsync(userId);

        if (user == null)
            return null;

        return new UserProfileDto
        {
            UserId = user.UserId,
           
            Email = user.Email,
            Phone = user.Phone
        };
    }

    // ================= CHANGE PASSWORD =================
    public async Task<ApiResponse> ChangePasswordAsync(int userId, ChangePasswordDto request)
    {
        var user = await _authRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);

        if (!isValidPassword)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Old password is incorrect"
            };
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _authRepository.UpdateUserAsync(user);

        return new ApiResponse
        {
            Success = true,
            Message = "Password changed successfully"
        };
    }

    // ================= JWT GENERATION =================
    private string GenerateJwtToken(User user)
    {
        var jwt = _config.GetSection("Jwt");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwt["DurationInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
