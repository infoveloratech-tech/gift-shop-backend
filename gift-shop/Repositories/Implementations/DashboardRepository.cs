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
        return await _context.Orders.SumAsync(o => o.TotalAmount);
    }

    /// <summary>
    /// Get revenue for the current month
    /// </summary>
    public async Task<decimal> GetMonthlySalesAsync()
    {
        var currentMonth = DateTime.UtcNow.AddMonths(-1);
        return await _context.Orders
            .Where(o => o.OrderDate >= currentMonth)
            .SumAsync(o => o.TotalAmount);
    }

    /// <summary>
    /// Get revenue for the current week
    /// </summary>
    public async Task<decimal> GetWeeklySalesAsync()
    {
        var currentWeek = DateTime.UtcNow.AddDays(-7);
        return await _context.Orders
            .Where(o => o.OrderDate >= currentWeek)
            .SumAsync(o => o.TotalAmount);
    }

    /// <summary>
    /// Get revenue for today
    /// </summary>
    public async Task<decimal> GetDailySalesAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Orders
            .Where(o => o.OrderDate.Date == today)
            .SumAsync(o => o.TotalAmount);
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
            .Where(i => i.QuantityOnHand <= i.ReorderLevel)
            .CountAsync();
    }

    /// <summary>
    /// Get top products by revenue
    /// </summary>
    public async Task<List<dynamic>> GetTopProductsByRevenueAsync(int count = 5)
    {
        var topProducts = await _context.OrderItems
            .GroupBy(oi => oi.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalSold = g.Sum(oi => oi.Quantity),
                Revenue = g.Sum(oi => oi.TotalPrice)
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
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .Select(o => new
            {
                o.Id,
                o.CustomerId,
                o.OrderDate,
                o.TotalAmount,
                o.Status
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
            .Where(o => o.OrderDate >= startDate)
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new
            {
                Date = g.Key,
                Amount = g.Sum(o => o.TotalAmount)
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
            .GroupBy(o => o.Status)
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
            .Where(i => i.QuantityOnHand <= i.ReorderLevel)
            .CountAsync();
        var totalStockValue = await _context.Inventories
            .Join(_context.Products, i => i.ProductId, p => p.Id, (i, p) => i.QuantityOnHand * p.Price)
            .SumAsync();

        return new
        {
            TotalItems = totalItems,
            LowStockItems = lowStockItems,
            TotalStockValue = totalStockValue
        };
    }
}
