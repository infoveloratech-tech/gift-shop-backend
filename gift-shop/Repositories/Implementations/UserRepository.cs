using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(GiftShopDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _dbSet.Where(u => u.Status == "Active").ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
    {
        return await _dbSet.Where(u => u.RoleId == roleId).ToListAsync();
    }

    public async Task<bool> UserEmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> VerifyPasswordAsync(int userId, string password)
    {
        var user = await GetByIdAsync(userId);
        if (user == null) return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string passwordHash)
    {
        var user = await GetByIdAsync(userId);
        if (user == null) return false;

        user.PasswordHash = passwordHash;
        user.UpdatedAt = DateTime.UtcNow;

        await UpdateAsync(user);
        await SaveChangesAsync();
        return true;
    }
}
