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
            .GroupBy(oi => oi.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalSold = g.Sum(oi => oi.Quantity),
                Revenue = g.Sum(oi => oi.TotalPrice)
            })
            .OrderByDescending(x => x.Revenue)
            .Take(topCount)
            .ToListAsync();

        var result = new List<TopProductDto>();
        foreach (var item in topProducts)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
            if (product != null)
            {
                result.Add(new TopProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
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
            .OrderByDescending(o => o.OrderDate)
            .Take(orderCount)
            .ToListAsync();

        var result = new List<RecentOrderDto>();
        foreach (var order in orders)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
            result.Add(new RecentOrderDto
            {
                Id = order.Id,
                CustomerName = customer != null ? $"{customer.FirstName} {customer.LastName}" : "Unknown",
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate
            });
        }

        return result;
    }

    public async Task<decimal> GetMonthlySalesAsync()
    {
        var currentMonth = DateTime.UtcNow.AddMonths(-1);
        return await _context.Orders
            .Where(o => o.OrderDate >= currentMonth)
            .SumAsync(o => o.TotalAmount);
    }

    public async Task<decimal> GetWeeklySalesAsync()
    {
        var currentWeek = DateTime.UtcNow.AddDays(-7);
        return await _context.Orders
            .Where(o => o.OrderDate >= currentWeek)
            .SumAsync(o => o.TotalAmount);
    }

    public async Task<decimal> GetDailySalesAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Orders
            .Where(o => o.OrderDate.Date == today)
            .SumAsync(o => o.TotalAmount);
    }

    public async Task<int> GetLowStockItemsCountAsync()
    {
        return await _context.Inventories
            .Where(i => i.QuantityOnHand <= i.ReorderLevel)
            .CountAsync();
    }

    public async Task<List<SalesTrendDto>> GetSalesTrendAsync(int days = 30)
    {
        var startDate = DateTime.UtcNow.AddDays(-days);

        var salesData = await _context.Orders
            .Where(o => o.OrderDate >= startDate)
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new SalesTrendDto
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Amount = g.Sum(o => o.TotalAmount)
            })
            .OrderBy(s => s.Date)
            .ToListAsync();

        return salesData;
    }

    private async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.Orders.SumAsync(o => o.TotalAmount);
    }

    private async Task<int> GetTotalOrdersCountAsync()
    {
        return await _context.Orders.CountAsync();
    }
}
