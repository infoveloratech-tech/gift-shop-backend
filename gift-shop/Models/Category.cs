namespace gift_shop.Models;

public class Category
{
    public int category_id { get; set; }
    public string? category_name { get; set; } 
    public string? description { get; set; }
    public string? image_url { get; set; }
    public string? status { get; set; }
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
}
