using gift_shop.Models;

namespace gift_shop.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId);
    Task<bool> UserEmailExistsAsync(string email);
    Task<bool> VerifyPasswordAsync(int userId, string password);
    Task<bool> UpdatePasswordAsync(int userId, string passwordHash);
}
