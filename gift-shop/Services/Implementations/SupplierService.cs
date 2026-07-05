using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
    {
        return await _supplierRepository.GetAllAsync();
    }

    public async Task<Supplier?> GetSupplierByIdAsync(int id)
    {
        return await _supplierRepository.GetByIdAsync(id);
    }

    public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
    {
        supplier.created_at = DateTime.UtcNow;
        supplier.updated_at = DateTime.UtcNow;

        if (string.IsNullOrEmpty(supplier.status))
            supplier.status = "active";

        return await _supplierRepository.CreateAsync(supplier);
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        var existing = await _supplierRepository.GetByIdAsync(supplier.supplier_id);
        if (existing == null) return false;

        supplier.updated_at = DateTime.UtcNow;

        return await _supplierRepository.UpdateAsync(supplier);
    }

    public async Task<bool> DeleteSupplierAsync(int id)
    {
        var existing = await _supplierRepository.GetByIdAsync(id);
        if (existing == null) return false;

        return await _supplierRepository.DeleteAsync(id);
    }
}