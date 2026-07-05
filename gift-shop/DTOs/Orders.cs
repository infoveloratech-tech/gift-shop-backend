namespace gift_shop.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
}

public class UpdateOrderDto
{
    public string Status { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
