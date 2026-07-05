using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // default values if needed
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.Status = user.Status ?? "active";

        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var existing = await _userRepository.GetByIdAsync(user.UserId);
        if (existing == null) return false;

        user.UpdatedAt = DateTime.UtcNow;

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var existing = await _userRepository.GetByIdAsync(id);
        if (existing == null) return false;

        return await _userRepository.DeleteAsync(id);
    }
}
