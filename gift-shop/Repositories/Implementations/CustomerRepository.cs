using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class CustomerRepository : Repository<Customer>, IRepository<Customer>
{
    public CustomerRepository(GiftShopDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _dbSet.Where(c => c.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetByCityAsync(string city)
    {
        return await _dbSet.Where(c => c.City == city).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetByCountryAsync(string country)
    {
        return await _dbSet.Where(c => c.Country == country).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
    {
        return await _dbSet.Where(c => c.FirstName.Contains(searchTerm) || 
                                       c.LastName.Contains(searchTerm) || 
                                       c.Email.Contains(searchTerm))
                           .ToListAsync();
    }

    public async Task<int> GetTotalActiveCustomersAsync()
    {
        return await _dbSet.Where(c => c.IsActive).CountAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(c => c.Email == email);
    }

    public async Task<bool> DeactivateCustomerAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer == null) return false;

        customer.IsActive = false;
        customer.UpdatedAt = DateTime.UtcNow;

        await UpdateAsync(customer);
        await SaveChangesAsync();
        return true;
    }

    public async Task<int> GetOrderCountByCustomerAsync(int customerId)
    {
        return await _context.Orders.Where(o => o.CustomerId == customerId).CountAsync();
    }

    public async Task<decimal> GetTotalSpentByCustomerAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .SumAsync(o => o.TotalAmount);
    }
}
