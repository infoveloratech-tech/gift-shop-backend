using gift_shop.Models;

namespace gift_shop.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);

    Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
    Task<object?> GetPaymentAsync(int orderId);
    Task<object?> GetShippingAsync(int orderId);
}