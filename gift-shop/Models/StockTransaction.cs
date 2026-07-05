namespace gift_shop.Models;

public class StockTransaction
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public int QuantityChange { get; set; } // Positive for stock in, negative for stock out
    public string TransactionType { get; set; } = string.Empty; // Purchase, Sale, Adjustment, Return, Damage
    public string Reference { get; set; } = string.Empty; // Order ID or Purchase Order ID
    public string Notes { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
