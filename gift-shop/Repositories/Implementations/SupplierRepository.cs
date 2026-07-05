using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class SupplierRepository : ISupplierRepository
{
    private readonly GiftShopDbContext _context;

    public SupplierRepository(GiftShopDbContext context)
    {
        _context = context;
    }

    // =========================
    // Get all suppliers
    // =========================
    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers
            .OrderByDescending(s => s.supplier_id)
            .ToListAsync();
    }

    // =========================
    // Get by id
    // =========================
    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers
            .FirstOrDefaultAsync(s => s.supplier_id == id);
    }

    // =========================
    // Create supplier
    // =========================
    public async Task<Supplier> CreateAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    // =========================
    // Update supplier
    // =========================
    public async Task<bool> UpdateAsync(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        return await _context.SaveChangesAsync() > 0;
    }

    // =========================
    // Delete supplier
    // =========================
    public async Task<bool> DeleteAsync(int id)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(s => s.supplier_id == id);

        if (supplier == null)
            return false;

        _context.Suppliers.Remove(supplier);
        return await _context.SaveChangesAsync() > 0;
    }
}