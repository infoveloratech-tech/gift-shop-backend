using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(GiftShopDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetByCustomerAsync(int customerId)
    {
        return await _dbSet.Where(o => o.CustomerId == customerId)
                           .OrderByDescending(o => o.OrderDate)
                           .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
    {
        return await _dbSet.Where(o => o.Status == status)
                           .OrderByDescending(o => o.OrderDate)
                           .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                           .OrderByDescending(o => o.OrderDate)
                           .ToListAsync();
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _dbSet.SumAsync(o => o.TotalAmount);
    }

    public async Task<decimal> GetRevenueByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                           .SumAsync(o => o.TotalAmount);
    }

    public async Task<int> GetTotalOrderCountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<int> GetOrderCountByStatusAsync(string status)
    {
        return await _dbSet.Where(o => o.Status == status).CountAsync();
    }

    public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int count)
    {
        return await _dbSet.OrderByDescending(o => o.OrderDate)
                           .Take(count)
                           .ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsAsync(int orderId)
    {
        return await _dbSet.FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await GetByIdAsync(orderId);
        if (order == null) return false;

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;

        await UpdateAsync(order);
        await SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetAverageOrderValueAsync()
    {
        var count = await GetTotalOrderCountAsync();
        if (count == 0) return 0;

        var total = await GetTotalRevenueAsync();
        return total / count;
    }

    public async Task<int> GetOrderCountByCustomerAsync(int customerId)
    {
        return await _dbSet.Where(o => o.CustomerId == customerId).CountAsync();
    }

    public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
    {
        return await _dbSet.Where(o => o.Status == "Pending")
                           .OrderByDescending(o => o.OrderDate)
                           .ToListAsync();
    }
}
