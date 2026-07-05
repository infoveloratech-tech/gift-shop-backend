namespace gift_shop.DTOs;

public class DashboardDto
{
    public DashboardSummaryDto Summary { get; set; } = new();
    public DashboardSalesDto Sales { get; set; } = new();
    public List<TopProductDto> TopProducts { get; set; } = new();
    public List<RecentOrderDto> RecentOrders { get; set; } = new();
}

public class DashboardSummaryDto
{
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalProducts { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int LowStockItems { get; set; }
}

public class DashboardSalesDto
{
    public decimal MonthlySales { get; set; }
    public decimal WeeklySales { get; set; }
    public decimal DailySales { get; set; }
    public List<SalesTrendDto> SalesTrend { get; set; } = new();
}

public class SalesTrendDto
{
    public string Date { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class TopProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalSold { get; set; }
    public decimal Revenue { get; set; }
}

public class RecentOrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
}
