using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using AutoMapper;

namespace gift_shop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    // =========================
    // Get all orders
    // =========================
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    // =========================
    // Get order by id
    // =========================
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    // =========================
    // Create order
    // =========================
    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.created_at = DateTime.UtcNow;
        order.updated_at = DateTime.UtcNow;

        // default status
        if (string.IsNullOrEmpty(order.order_status))
            order.order_status = "pending";

        return await _orderRepository.CreateAsync(order);
    }

    // =========================
    // Update order
    // =========================
    public async Task<bool> UpdateOrderAsync(Order order)
    {
        var existing = await _orderRepository.GetByIdAsync(order.order_id);
        if (existing == null) return false;

        order.updated_at = DateTime.UtcNow;

        return await _orderRepository.UpdateAsync(order);
    }

    // =========================
    // Delete order
    // =========================
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var existing = await _orderRepository.GetByIdAsync(id);
        if (existing == null) return false;

        return await _orderRepository.DeleteAsync(id);
    }

    // =========================
    // Orders by user
    // =========================
    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }

    // =========================
    // Order details (basic version)
    // =========================
    public async Task<object?> GetOrderDetailsAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) return null;

        var items = await _orderRepository.GetOrderItemsAsync(orderId);
        var payment = await _orderRepository.GetPaymentAsync(orderId);
        var shipping = await _orderRepository.GetShippingAsync(orderId);

        return new
        {
            order,
            items,
            payment,
            shipping
        };
    }
}