using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(GiftShopDbContext context) : base(context)
    {
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet.Where(p => p.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId)
    {
        return await _dbSet.Where(p => p.SupplierId == supplierId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _dbSet.Where(p => p.Name.Contains(searchTerm) || 
                                       p.Description.Contains(searchTerm) || 
                                       p.Sku.Contains(searchTerm))
                           .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _dbSet.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                           .ToListAsync();
    }

    public async Task<bool> SkuExistsAsync(string sku)
    {
        return await _dbSet.AnyAsync(p => p.Sku == sku);
    }

    public async Task<int> GetTotalProductCountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        var products = await _dbSet.ToListAsync();
        var productIds = products.Select(p => p.Id).ToList();

        var lowStockProductIds = _context.Inventories
            .Where(i => i.QuantityOnHand <= threshold)
            .Select(i => i.ProductId)
            .ToList();

        return products.Where(p => lowStockProductIds.Contains(p.Id)).ToList();
    }
}
