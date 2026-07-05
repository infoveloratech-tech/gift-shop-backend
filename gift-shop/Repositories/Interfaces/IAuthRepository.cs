using gift_shop.Models;

namespace gift_shop.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(int userId);

        Task<bool> EmailExistsAsync(string email);

        Task<User> AddUserAsync(User user);

        Task UpdateUserAsync(User user);
    }
}
