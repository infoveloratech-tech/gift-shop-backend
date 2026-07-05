namespace gift_shop.Models;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // Credit Card, Debit Card, PayPal, etc.
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded
    public string TransactionId { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
