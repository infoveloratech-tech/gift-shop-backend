using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId);
    Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto);
    Task<bool> CancelOrderAsync(int id);
    Task<bool> DeleteOrderAsync(int id);
    Task<OrderDto> UpdateOrderStatusAsync(int id, string status);
    Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalRevenueAsync();
    Task<int> GetTotalOrdersCountAsync();
}
