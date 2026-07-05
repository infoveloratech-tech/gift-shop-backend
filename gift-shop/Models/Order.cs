namespace gift_shop.Models;

public class Order
{
    public int order_id { get; set; }
    public int user_id { get; set; }
    public DateTime order_date { get; set; } = DateTime.UtcNow;
    public decimal total_amount { get; set; }
    public string order_status { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
}
