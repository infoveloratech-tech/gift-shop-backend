using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // =========================
        // GET: api/orders
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // =========================
        // GET: api/orders/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }

        // =========================
        // POST: api/orders
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOrder = await _orderService.CreateOrderAsync(order);

            return Ok(new
            {
                message = "Order created successfully",
                data = createdOrder
            });
        }

        // =========================
        // PUT: api/orders/{id}
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.order_id)
                return BadRequest(new { message = "Order ID mismatch" });

            var updated = await _orderService.UpdateOrderAsync(order);

            if (!updated)
                return NotFound(new { message = "Order not found" });

            return Ok(new { message = "Order updated successfully" });
        }

        // =========================
        // DELETE: api/orders/{id}
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);

            if (!deleted)
                return NotFound(new { message = "Order not found" });

            return Ok(new { message = "Order deleted successfully" });
        }

        // =========================
        // GET: api/orders/user/{userId}
        // =========================
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        // =========================
        // GET: api/orders/details/{id}
        // (Order + Items + Shipping + Payment)
        // =========================
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var orderDetails = await _orderService.GetOrderDetailsAsync(id);

            if (orderDetails == null)
                return NotFound(new { message = "Order details not found" });

            return Ok(orderDetails);
        }
    }
}
