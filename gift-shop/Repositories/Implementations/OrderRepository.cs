using gift_shop.Data;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly GiftShopDbContext _context;

    public OrderRepository(GiftShopDbContext context)
    {
        _context = context;
    }

    // =========================
    // Get all orders
    // =========================
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .OrderByDescending(o => o.order_id)
            .ToListAsync();
    }

    // =========================
    // Get order by id
    // =========================
    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.order_id == id);
    }

    // =========================
    // Create order
    // =========================
    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // =========================
    // Update order
    // =========================
    public async Task<bool> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        return await _context.SaveChangesAsync() > 0;
    }

    // =========================
    // Delete order
    // =========================
    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.order_id == id);

        if (order == null)
            return false;

        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync() > 0;
    }

    // =========================
    // Get orders by user
    // =========================
    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.user_id == userId)
            .OrderByDescending(o => o.order_id)
            .ToListAsync();
    }

    // =========================
    // Get order items
    // =========================
    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.order_id == orderId)
            .ToListAsync();
    }

    // =========================
    // Get payment (latest or single)
    // =========================
    public async Task<object?> GetPaymentAsync(int orderId)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    // =========================
    // Get shipping
    // =========================
    public async Task<object?> GetShippingAsync(int orderId)
    {
        return await _context.Shipping
            .FirstOrDefaultAsync(s => s.order_id == orderId);
    }
}