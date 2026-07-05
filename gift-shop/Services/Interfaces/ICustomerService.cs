using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task<CustomerDto?> GetCustomerByEmailAsync(string email);
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<IEnumerable<CustomerDto>> GetActiveCustomersAsync();
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
    Task<bool> DeleteCustomerAsync(int id);
    Task<bool> DeactivateCustomerAsync(int id);
    Task<IEnumerable<CustomerDto>> GetCustomersByCityAsync(string city);
    Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm);
}
