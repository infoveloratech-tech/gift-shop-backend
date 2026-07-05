namespace gift_shop.Models;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
    public int ReorderLevel { get; set; }
    public string WarehouseLocation { get; set; } = string.Empty;
    public DateTime LastCountDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
