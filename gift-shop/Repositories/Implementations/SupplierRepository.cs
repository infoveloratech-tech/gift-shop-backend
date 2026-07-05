using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class SupplierRepository : Repository<Supplier>, IRepository<Supplier>
{
    public SupplierRepository(GiftShopDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync()
    {
        return await _dbSet.Where(s => s.IsActive).ToListAsync();
    }

    public async Task<Supplier?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<Supplier?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.ContactEmail == email);
    }

    public async Task<IEnumerable<Supplier>> GetByCityAsync(string city)
    {
        return await _dbSet.Where(s => s.City == city).ToListAsync();
    }

    public async Task<IEnumerable<Supplier>> GetByCountryAsync(string country)
    {
        return await _dbSet.Where(s => s.Country == country).ToListAsync();
    }

    public async Task<int> GetTotalActiveSuppliersAsync()
    {
        return await _dbSet.Where(s => s.IsActive).CountAsync();
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await _dbSet.AnyAsync(s => s.Name == name);
    }

    public async Task<bool> DeactivateSupplierAsync(int id)
    {
        var supplier = await GetByIdAsync(id);
        if (supplier == null) return false;

        supplier.IsActive = false;
        supplier.UpdatedAt = DateTime.UtcNow;

        await UpdateAsync(supplier);
        await SaveChangesAsync();
        return true;
    }

    public async Task<int> GetProductCountBySupplierAsync(int supplierId)
    {
        return await _context.Products.Where(p => p.SupplierId == supplierId).CountAsync();
    }
}
