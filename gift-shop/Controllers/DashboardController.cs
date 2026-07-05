using gift_shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly GiftShopDbContext _context;

        public DashboardController(GiftShopDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: api/dashboard/summary
        // =========================
        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();
            var totalCoupons = await _context.Coupons.CountAsync();

            var totalRevenue = await _context.Orders
                .Where(o => o.order_status == "completed")
                .SumAsync(o => (decimal?)o.total_amount) ?? 0;

            return Ok(new
            {
                totalUsers,
                totalProducts,
                totalOrders,
                totalCoupons,
                totalRevenue
            });
        }

        // =========================
        // GET: api/dashboard/recent-orders
        // =========================
        [HttpGet("recent-orders")]
        public async Task<IActionResult> GetRecentOrders()
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.order_id)
                .Take(10)
                .ToListAsync();

            return Ok(orders);
        }

        // =========================
        // GET: api/dashboard/top-products
        // =========================
        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts()
        {
            var topProducts = await _context.OrderItems
                .GroupBy(oi => oi.order_item_id)
                .Select(g => new
                {
                    product_id = g.Key,
                    total_sold = g.Sum(x => x.quantity)
                })
                .OrderByDescending(x => x.total_sold)
                .Take(5)
                .ToListAsync();

            return Ok(topProducts);
        }

        // =========================
        // GET: api/dashboard/inventory-alerts
        // =========================
        [HttpGet("inventory-alerts")]
        public async Task<IActionResult> GetInventoryAlerts()
        {
            var lowStock = await _context.Inventories
                .Where(i => i.quantity <= i.reorder_level)
                .ToListAsync();

            return Ok(lowStock);
        }
    }
}
