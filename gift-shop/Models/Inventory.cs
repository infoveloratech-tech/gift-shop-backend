namespace gift_shop.Models;

public class Inventory
{
    public int inventory_id { get; set; }
    public int product_id { get; set; }
    public int quantity { get; set; }
    public int reserved_quantity { get; set; }
    public int reorder_level { get; set; }
    public DateTime last_stock_update { get; set; } = DateTime.UtcNow;
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
}
