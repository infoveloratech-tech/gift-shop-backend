using gift_shop.DTOs;

namespace gift_shop.Validators;

public static class OrderValidator
{
    private static readonly string[] ValidOrderStatuses = { "Pending", "Processing", "Shipped", "Delivered", "Cancelled", "Refunded" };

    /// <summary>
    /// Validates CreateOrderDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateCreateOrder(CreateOrderDto dto)
    {
        if (dto.CustomerId <= 0)
            return (false, "Valid customer ID is required");

        if (string.IsNullOrWhiteSpace(dto.ShippingAddress))
            return (false, "Shipping address is required");

        if (dto.ShippingAddress.Length < 5 || dto.ShippingAddress.Length > 200)
            return (false, "Shipping address must be between 5 and 200 characters");

        if (dto.Items == null || dto.Items.Count == 0)
            return (false, "Order must contain at least one item");

        foreach (var item in dto.Items)
        {
            var itemValidation = ValidateOrderItem(item);
            if (!itemValidation.isValid)
                return itemValidation;
        }

        if (!string.IsNullOrWhiteSpace(dto.Notes) && dto.Notes.Length > 500)
            return (false, "Order notes cannot exceed 500 characters");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates UpdateOrderDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateUpdateOrder(UpdateOrderDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Status))
        {
            var (isValid, message) = ValidateOrderStatus(dto.Status);
            if (!isValid)
                return (false, message);
        }

        if (!string.IsNullOrWhiteSpace(dto.ShippingAddress))
        {
            if (dto.ShippingAddress.Length < 5 || dto.ShippingAddress.Length > 200)
                return (false, "Shipping address must be between 5 and 200 characters");
        }

        if (!string.IsNullOrWhiteSpace(dto.Notes) && dto.Notes.Length > 500)
            return (false, "Order notes cannot exceed 500 characters");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates order item
    /// </summary>
    private static (bool isValid, string errorMessage) ValidateOrderItem(OrderItemDto item)
    {
        if (item.ProductId <= 0)
            return (false, "Valid product ID is required");

        if (item.Quantity <= 0)
            return (false, "Order quantity must be greater than 0");

        if (item.Quantity > 10000)
            return (false, "Order quantity cannot exceed 10000");

        if (item.UnitPrice <= 0)
            return (false, "Unit price must be greater than 0");

        if (item.UnitPrice > 999999.99m)
            return (false, "Unit price exceeds maximum allowed value");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates order status
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateOrderStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return (false, "Order status is required");

        if (!ValidOrderStatuses.Contains(status))
            return (false, $"Invalid order status. Allowed values: {string.Join(", ", ValidOrderStatuses)}");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates total order amount
    /// </summary>
    public static bool IsValidOrderAmount(decimal amount)
    {
        return amount > 0 && amount <= 999999999.99m;
    }

    /// <summary>
    /// Validates order can be cancelled
    /// </summary>
    public static (bool canCancel, string message) CanCancelOrder(string currentStatus)
    {
        if (currentStatus == "Delivered" || currentStatus == "Cancelled" || currentStatus == "Refunded")
            return (false, $"Cannot cancel order with status '{currentStatus}'");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates status transition
    /// </summary>
    public static (bool isValid, string message) IsValidStatusTransition(string currentStatus, string newStatus)
    {
        var validTransitions = new Dictionary<string, string[]>
        {
            { "Pending", new[] { "Processing", "Cancelled" } },
            { "Processing", new[] { "Shipped", "Cancelled" } },
            { "Shipped", new[] { "Delivered", "Cancelled" } },
            { "Delivered", new[] { "Refunded" } },
            { "Cancelled", new string[] { } },
            { "Refunded", new string[] { } }
        };

        if (!validTransitions.ContainsKey(currentStatus))
            return (false, $"Unknown status: {currentStatus}");

        if (!validTransitions[currentStatus].Contains(newStatus))
            return (false, $"Cannot transition from {currentStatus} to {newStatus}");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates order contains required items
    /// </summary>
    public static bool HasValidOrderItems(List<OrderItemDto> items)
    {
        return items != null && items.Count > 0 && items.All(i => i.ProductId > 0 && i.Quantity > 0);
    }

    /// <summary>
    /// Gets all valid order statuses
    /// </summary>
    public static string[] GetValidOrderStatuses()
    {
        return ValidOrderStatuses;
    }
}
