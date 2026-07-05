using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly GiftShopDbContext _context;

    public SupplierService(GiftShopDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        return supplier == null ? null : MapToDto(supplier);
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers = await _context.Suppliers.ToListAsync();
        return suppliers.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<SupplierDto>> GetActiveSuppliersAsync()
    {
        var suppliers = await _context.Suppliers.Where(s => s.IsActive).ToListAsync();
        return suppliers.Select(MapToDto).ToList();
    }

    public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
    {
        var supplier = new Supplier
        {
            Name = createSupplierDto.Name,
            ContactEmail = createSupplierDto.ContactEmail,
            ContactPhone = createSupplierDto.ContactPhone,
            Address = createSupplierDto.Address,
            City = createSupplierDto.City,
            State = createSupplierDto.State,
            PostalCode = createSupplierDto.PostalCode,
            Country = createSupplierDto.Country,
            IsActive = true
        };

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<SupplierDto> UpdateSupplierAsync(int id, UpdateSupplierDto updateSupplierDto)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        if (supplier == null) throw new ArgumentException("Supplier not found");

        supplier.Name = updateSupplierDto.Name;
        supplier.ContactEmail = updateSupplierDto.ContactEmail;
        supplier.ContactPhone = updateSupplierDto.ContactPhone;
        supplier.Address = updateSupplierDto.Address;
        supplier.City = updateSupplierDto.City;
        supplier.State = updateSupplierDto.State;
        supplier.PostalCode = updateSupplierDto.PostalCode;
        supplier.Country = updateSupplierDto.Country;
        supplier.IsActive = updateSupplierDto.IsActive;
        supplier.UpdatedAt = DateTime.UtcNow;

        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<bool> DeleteSupplierAsync(int id)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        if (supplier == null) return false;

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateSupplierAsync(int id)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        if (supplier == null) return false;

        supplier.IsActive = false;
        supplier.UpdatedAt = DateTime.UtcNow;

        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersByCityAsync(string city)
    {
        var suppliers = await _context.Suppliers.Where(s => s.City == city).ToListAsync();
        return suppliers.Select(MapToDto).ToList();
    }

    private SupplierDto MapToDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            ContactEmail = supplier.ContactEmail,
            ContactPhone = supplier.ContactPhone,
            Address = supplier.Address,
            City = supplier.City,
            State = supplier.State,
            PostalCode = supplier.PostalCode,
            Country = supplier.Country,
            IsActive = supplier.IsActive,
            CreatedAt = supplier.CreatedAt
        };
    }
}
