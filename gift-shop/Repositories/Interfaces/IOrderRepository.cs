using gift_shop.Models;

namespace gift_shop.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetByStatusAsync(string status);
    Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalRevenueAsync();
    Task<decimal> GetRevenueByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<int> GetTotalOrderCountAsync();
    Task<int> GetOrderCountByStatusAsync(string status);
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int count);
    Task<Order?> GetOrderWithItemsAsync(int orderId);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
}
