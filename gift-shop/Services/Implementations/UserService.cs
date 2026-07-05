using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class UserService : IUserService
{
    private readonly GiftShopDbContext _context;

    public UserService(GiftShopDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RoleId == id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user == null ? null : MapToDto(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(MapToDto).ToList();
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            Phone = createUserDto.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            RoleId = 4, // Default to Customer role
            Status = "Active"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return MapToDto(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RoleId == id);
        if (user == null) throw new ArgumentException("User not found");

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.Phone = updateUserDto.PhoneNumber;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return MapToDto(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RoleId == id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RoleId == userId);
        if (user == null) return false;

        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            return false;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private UserDto MapToDto(User user)
    {
        var role = _context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
        return new UserDto
        {
            Id = user.RoleId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Role = role?.Name ?? "User",
            status= user.Status,
            CreatedAt = user.CreatedAt
        };
    }
}
