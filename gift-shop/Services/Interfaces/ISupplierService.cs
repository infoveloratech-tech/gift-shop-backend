using gift_shop.DTOs;
using gift_shop.Models;

namespace gift_shop.Services.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
    Task<Supplier?> GetSupplierByIdAsync(int id);
    Task<Supplier> CreateSupplierAsync(Supplier supplier);
    Task<bool> UpdateSupplierAsync(Supplier supplier);
    Task<bool> DeleteSupplierAsync(int id);
}