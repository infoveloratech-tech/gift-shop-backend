using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces;

public interface ISupplierService
{
    Task<SupplierDto?> GetSupplierByIdAsync(int id);
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
    Task<IEnumerable<SupplierDto>> GetActiveSuppliersAsync();
    Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
    Task<SupplierDto> UpdateSupplierAsync(int id, UpdateSupplierDto updateSupplierDto);
    Task<bool> DeleteSupplierAsync(int id);
    Task<bool> DeactivateSupplierAsync(int id);
    Task<IEnumerable<SupplierDto>> GetSuppliersByCityAsync(string city);
}
