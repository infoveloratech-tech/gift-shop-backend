using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly GiftShopDbContext _context;

    public DashboardService(GiftShopDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var dashboard = new DashboardDto
        {
            Summary = await GetSummaryAsync(),
            Sales = await GetSalesAsync(),
            TopProducts = await GetTopProductsAsync(),
            RecentOrders = await GetRecentOrdersAsync()
        };

        return dashboard;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var totalRevenue = await GetTotalRevenueAsync();
        var totalOrders = await GetTotalOrdersCountAsync();
        var totalCustomers = await _context.Customers.CountAsync();
        var totalProducts = await _context.Products.CountAsync();
        var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;
        var lowStockItems = await GetLowStockItemsCountAsync();

        return new DashboardSummaryDto
        {
            TotalRevenue = totalRevenue,
            TotalOrders = totalOrders,
            TotalCustomers = totalCustomers,
            TotalProducts = totalProducts,
            AverageOrderValue = averageOrderValue,
            LowStockItems = lowStockItems
        };
    }

    public async Task<DashboardSalesDto> GetSalesAsync()
    {
        var monthlySales = await GetMonthlySalesAsync();
        var weeklySales = await GetWeeklySalesAsync();
        var dailySales = await GetDailySalesAsync();
        var salesTrend = await GetSalesTrendAsync(30);

        return new DashboardSalesDto
        {
            MonthlySales = monthlySales,
            WeeklySales = weeklySales,
            DailySales = dailySales,
            SalesTrend = salesTrend
        };
    }

    public async Task<List<TopProductDto>> GetTopProductsAsync(int topCount = 5)
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
            .Take(topCount)
            .ToListAsync();

        var result = new List<TopProductDto>();
        foreach (var item in topProducts)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.product_id == item.ProductId);
            if (product != null)
            {
                result.Add(new TopProductDto
                {
                    Id = product.product_id,
                    Name = product.product_name,
                    TotalSold = item.TotalSold,
                    Revenue = item.Revenue
                });
            }
        }

        return result;
    }

    public async Task<List<RecentOrderDto>> GetRecentOrdersAsync(int orderCount = 10)
    {
        var orders = await _context.Orders
            .OrderByDescending(o => o.order_date)
            .Take(orderCount)
            .ToListAsync();

        var result = new List<RecentOrderDto>();
        foreach (var order in orders)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == order.user_id);
            result.Add(new RecentOrderDto
            {
                Id = order.order_id,
                CustomerName = customer != null ? $"{customer.FirstName} {customer.LastName}" : "Unknown",
                TotalAmount = order.total_amount,
                Status = order.order_status,
                OrderDate = order.order_date
            });
        }

        return result;
    }

    public async Task<decimal> GetMonthlySalesAsync()
    {
        var currentMonth = DateTime.UtcNow.AddMonths(-1);
        return await _context.Orders
            .Where(o => o.order_date >= currentMonth)
            .SumAsync(o => o.total_amount);
    }

    public async Task<decimal> GetWeeklySalesAsync()
    {
        var currentWeek = DateTime.UtcNow.AddDays(-7);
        return await _context.Orders
            .Where(o => o.order_date >= currentWeek)
            .SumAsync(o => o.total_amount);
    }

    public async Task<decimal> GetDailySalesAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Orders
            .Where(o => o.order_date.Date == today)
            .SumAsync(o => o.total_amount);
    }

    public async Task<int> GetLowStockItemsCountAsync()
    {
        return await _context.Inventories
            .Where(i => i.quantity <= i.reorder_level)
            .CountAsync();
    }

    public async Task<List<SalesTrendDto>> GetSalesTrendAsync(int days = 30)
    {
        var startDate = DateTime.UtcNow.AddDays(-days);

        var salesData = await _context.Orders
            .Where(o => o.order_date >= startDate)
            .GroupBy(o => o.order_date.Date)
            .Select(g => new SalesTrendDto
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Amount = g.Sum(o => o.total_amount)
            })
            .OrderBy(s => s.Date)
            .ToListAsync();

        return salesData;
    }

    private async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.Orders.SumAsync(o => o.total_amount);
    }

    private async Task<int> GetTotalOrdersCountAsync()
    {
        return await _context.Orders.CountAsync();
    }
}
