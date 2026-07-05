using gift_shop.Data;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class DashboardRepository
{
    private readonly GiftShopDbContext _context;

    public DashboardRepository(GiftShopDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get total revenue from all orders
    /// </summary>
    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.Orders.SumAsync(o => o.total_amount);
    }

    /// <summary>
    /// Get revenue for the current month
    /// </summary>
    public async Task<decimal> GetMonthlySalesAsync()
    {
        var currentMonth = DateTime.UtcNow.AddMonths(-1);
        return await _context.Orders
            .Where(o => o.order_date >= currentMonth)
            .SumAsync(o => o.total_amount);
    }

    /// <summary>
    /// Get revenue for the current week
    /// </summary>
    public async Task<decimal> GetWeeklySalesAsync()
    {
        var currentWeek = DateTime.UtcNow.AddDays(-7);
        return await _context.Orders
            .Where(o => o.order_date >= currentWeek)
            .SumAsync(o => o.total_amount);
    }

    /// <summary>
    /// Get revenue for today
    /// </summary>
    public async Task<decimal> GetDailySalesAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Orders
            .Where(o => o.order_date.Date == today)
            .SumAsync(o => o.total_amount);
    }

    /// <summary>
    /// Get total number of orders
    /// </summary>
    public async Task<int> GetTotalOrdersAsync()
    {
        return await _context.Orders.CountAsync();
    }

    /// <summary>
    /// Get total number of customers
    /// </summary>
    public async Task<int> GetTotalCustomersAsync()
    {
        return await _context.Customers.CountAsync();
    }

    /// <summary>
    /// Get total number of products
    /// </summary>
    public async Task<int> GetTotalProductsAsync()
    {
        return await _context.Products.CountAsync();
    }

    /// <summary>
    /// Get average order value
    /// </summary>
    public async Task<decimal> GetAverageOrderValueAsync()
    {
        var count = await GetTotalOrdersAsync();
        if (count == 0) return 0;

        var total = await GetTotalRevenueAsync();
        return total / count;
    }

    /// <summary>
    /// Get count of low stock items
    /// </summary>
    public async Task<int> GetLowStockItemsAsync()
    {
        return await _context.Inventories
            .Where(i => i.quantity <= i.reorder_level)
            .CountAsync();
    }

    /// <summary>
    /// Get top products by revenue
    /// </summary>
    public async Task<List<dynamic>> GetTopProductsByRevenueAsync(int count = 5)
    {
        var topProducts = await _context.OrderItems
            .GroupBy(oi => oi.product_id)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalSold = g.Sum(oi => oi.quantity),
                Revenue = g.Sum(oi => oi.total)
            })
            .OrderByDescending(x => x.Revenue)
            .Take(count)
            .ToListAsync();

        return topProducts.Cast<dynamic>().ToList();
    }

    /// <summary>
    /// Get recent orders
    /// </summary>
    public async Task<List<dynamic>> GetRecentOrdersAsync(int count = 10)
    {
        var orders = await _context.Orders
            .OrderByDescending(o => o.order_date)
            .Take(count)
            .Select(o => new
            {
                o.order_id,
                o.user_id,
                o.order_date,
                o.total_amount,
                o.order_status
            })
            .ToListAsync();

        return orders.Cast<dynamic>().ToList();
    }

    /// <summary>
    /// Get sales trend for the specified number of days
    /// </summary>
    public async Task<List<dynamic>> GetSalesTrendAsync(int days = 30)
    {
        var startDate = DateTime.UtcNow.AddDays(-days);

        var salesTrend = await _context.Orders
            .Where(o => o.order_date >= startDate)
            .GroupBy(o => o.order_date.Date)
            .Select(g => new
            {
                Date = g.Key,
                Amount = g.Sum(o => o.total_amount)
            })
            .OrderBy(s => s.Date)
            .ToListAsync();

        return salesTrend.Cast<dynamic>().ToList();
    }

    /// <summary>
    /// Get order count by status
    /// </summary>
    public async Task<List<dynamic>> GetOrderCountByStatusAsync()
    {
        var ordersByStatus = await _context.Orders
            .GroupBy(o => o.order_status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return ordersByStatus.Cast<dynamic>().ToList();
    }

    /// <summary>
    /// Get customer statistics
    /// </summary>
    public async Task<dynamic> GetCustomerStatsAsync()
    {
        var activeCustomers = await _context.Customers.Where(c => c.IsActive).CountAsync();
        var totalCustomers = await _context.Customers.CountAsync();
        var newCustomersThisMonth = await _context.Customers
            .Where(c => c.CreatedAt >= DateTime.UtcNow.AddMonths(-1))
            .CountAsync();

        return new
        {
            TotalCustomers = totalCustomers,
            ActiveCustomers = activeCustomers,
            NewCustomersThisMonth = newCustomersThisMonth
        };
    }

    /// <summary>
    /// Get inventory statistics
    /// </summary>
    public async Task<dynamic> GetInventoryStatsAsync()
    {
        var totalItems = await _context.Inventories.CountAsync();
        var lowStockItems = await _context.Inventories
            .Where(i => i.quantity <= i.reorder_level)
            .CountAsync();
        var totalStockValue = await _context.Inventories
            .Join(_context.Products, i => i.product_id, p => p.product_id, (i, p) => i.quantity * p.price)
            .SumAsync();

        return new
        {
            TotalItems = totalItems,
            LowStockItems = lowStockItems,
            TotalStockValue = totalStockValue
        };
    }
}
