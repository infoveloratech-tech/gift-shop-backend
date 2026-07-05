using gift_shop.DTOs.Auth;

namespace gift_shop.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);

    Task<ApiResponse> RegisterAsync(RegisterRequest request);

    Task<LoginResponse?> GoogleLoginAsync(GoogleLoginRequest request);

    Task<UserProfileDto?> GetProfileAsync(int userId);

    Task<ApiResponse> ChangePasswordAsync(int userId, ChangePasswordDto request);
}