using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GiftShopDbContext _context;

        public AuthRepository(GiftShopDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (existingUser == null)
                throw new Exception("User not found");

            // Map fields
            existingUser.RoleId = user.RoleId;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Gender = user.Gender;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.ProfileImage = user.ProfileImage;
            existingUser.EmailVerified = user.EmailVerified;
            existingUser.PhoneVerified = user.PhoneVerified;
            existingUser.Status = user.Status;
            existingUser.LastLogin = user.LastLogin;
            existingUser.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
