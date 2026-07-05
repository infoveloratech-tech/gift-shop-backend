using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
    Task<DashboardSummaryDto> GetSummaryAsync();
    Task<DashboardSalesDto> GetSalesAsync();
    Task<List<TopProductDto>> GetTopProductsAsync(int topCount = 5);
    Task<List<RecentOrderDto>> GetRecentOrdersAsync(int orderCount = 10);
    Task<decimal> GetMonthlySalesAsync();
    Task<decimal> GetWeeklySalesAsync();
    Task<decimal> GetDailySalesAsync();
    Task<int> GetLowStockItemsCountAsync();
    Task<List<SalesTrendDto>> GetSalesTrendAsync(int days = 30);
}
