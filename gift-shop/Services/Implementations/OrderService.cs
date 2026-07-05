using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using AutoMapper;

namespace gift_shop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order == null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId)
    {
        var orders = await _orderRepository.GetByCustomerAsync(customerId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
    {
        var orders = await _orderRepository.GetByStatusAsync(status);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var order = new Order
        {
            CustomerId = createOrderDto.CustomerId,
            ShippingAddress = createOrderDto.ShippingAddress,
            Notes = createOrderDto.Notes,
            Status = "Pending",
            TotalAmount = createOrderDto.Items.Sum(i => i.Quantity * i.UnitPrice),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new InvalidOperationException($"Order with id {id} not found");

        order.Status = updateOrderDto.Status;
        order.ShippingAddress = updateOrderDto.ShippingAddress;
        order.Notes = updateOrderDto.Notes;
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<bool> CancelOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return false;

        if (order.Status == "Cancelled" || order.Status == "Delivered")
            return false;

        order.Status = "Cancelled";
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return false;

        var result = await _orderRepository.DeleteAsync(id);
        if (result)
            await _orderRepository.SaveChangesAsync();
        return result;
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(int id, string status)
    {
        var result = await _orderRepository.UpdateOrderStatusAsync(id, status);
        if (!result)
            throw new InvalidOperationException($"Failed to update order status for id {id}");

        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new InvalidOperationException($"Order with id {id} not found");

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var orders = await _orderRepository.GetByDateRangeAsync(startDate, endDate);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _orderRepository.GetTotalRevenueAsync();
    }

    public async Task<int> GetTotalOrdersCountAsync()
    {
        return await _orderRepository.GetTotalOrderCountAsync();
    }
}
