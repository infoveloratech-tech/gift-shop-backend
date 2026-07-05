using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly GiftShopDbContext _context;

    public CustomerService(GiftShopDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        return customer == null ? null : MapToDto(customer);
    }

    public async Task<CustomerDto?> GetCustomerByEmailAsync(string email)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        return customer == null ? null : MapToDto(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return customers.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<CustomerDto>> GetActiveCustomersAsync()
    {
        var customers = await _context.Customers.Where(c => c.IsActive).ToListAsync();
        return customers.Select(MapToDto).ToList();
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var customer = new Customer
        {
            FirstName = createCustomerDto.FirstName,
            LastName = createCustomerDto.LastName,
            Email = createCustomerDto.Email,
            PhoneNumber = createCustomerDto.PhoneNumber,
            Address = createCustomerDto.Address,
            City = createCustomerDto.City,
            State = createCustomerDto.State,
            PostalCode = createCustomerDto.PostalCode,
            Country = createCustomerDto.Country,
            IsActive = true
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return MapToDto(customer);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null) throw new ArgumentException("Customer not found");

        customer.FirstName = updateCustomerDto.FirstName;
        customer.LastName = updateCustomerDto.LastName;
        customer.PhoneNumber = updateCustomerDto.PhoneNumber;
        customer.Address = updateCustomerDto.Address;
        customer.City = updateCustomerDto.City;
        customer.State = updateCustomerDto.State;
        customer.PostalCode = updateCustomerDto.PostalCode;
        customer.Country = updateCustomerDto.Country;
        customer.UpdatedAt = DateTime.UtcNow;

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return MapToDto(customer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateCustomerAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null) return false;

        customer.IsActive = false;
        customer.UpdatedAt = DateTime.UtcNow;

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersByCityAsync(string city)
    {
        var customers = await _context.Customers.Where(c => c.City == city).ToListAsync();
        return customers.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm)
    {
        var customers = await _context.Customers
            .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm) || c.Email.Contains(searchTerm))
            .ToListAsync();
        return customers.Select(MapToDto).ToList();
    }

    private CustomerDto MapToDto(Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            City = customer.City,
            State = customer.State,
            PostalCode = customer.PostalCode,
            Country = customer.Country,
            IsActive = customer.IsActive,
            CreatedAt = customer.CreatedAt
        };
    }
}
