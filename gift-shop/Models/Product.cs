namespace gift_shop.Models;

public class Product
{
    public int product_id { get; set; }
    public string product_name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public decimal price { get; set; }
    public decimal cost_price { get; set; }
    public int category_id { get; set; }
    public int supplier_id { get; set; }
    public string sku { get; set; } = string.Empty;
    //public string ImageUrl { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
}
