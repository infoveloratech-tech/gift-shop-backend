namespace gift_shop.Models;

public class OrderItem
{
    public int order_item_id { get; set; }
    public int order_id { get; set; }
    public int product_id { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public decimal total { get; set; }
    public DateTime created_at { get; set; } = DateTime.UtcNow;
}
