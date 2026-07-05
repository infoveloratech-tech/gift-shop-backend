using gift_shop.DTOs;
using gift_shop.Models;

namespace gift_shop.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task<bool> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(int id);

    Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
    Task<object?> GetOrderDetailsAsync(int orderId);
}