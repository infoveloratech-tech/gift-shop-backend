using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly GiftShopDbContext _context;

    public ProductRepository(GiftShopDbContext context)
    {
        _context = context;
    }

    // =========================
    // Get all products
    // =========================
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .OrderByDescending(p => p.product_id)
            .ToListAsync();
    }

    // =========================
    // Get by id
    // =========================
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.product_id == id);
    }

    // =========================
    // Create product
    // =========================
    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    // =========================
    // Update product
    // =========================
    public async Task<bool> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }

    // =========================
    // Delete product
    // =========================
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.product_id == id);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        return await _context.SaveChangesAsync() > 0;
    }

    // =========================
    // Get by category
    // =========================
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.category_id == categoryId)
            .ToListAsync();
    }

    // =========================
    // Search products
    // =========================
    public async Task<IEnumerable<Product>> SearchAsync(string name)
    {
        return await _context.Products
            .Where(p => p.product_name.Contains(name))
            .ToListAsync();
    }
}