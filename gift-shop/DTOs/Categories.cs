using System.ComponentModel.DataAnnotations;

namespace gift_shop.DTOs;


    public class CategoryDto
{
    public int category_id { get; set; }

    public string category_name { get; set; } = string.Empty;

    public string? description { get; set; }
    public string? image_url { get; set; }
    public string? status { get; set; }
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime statusupdated_at { get; set; } = DateTime.UtcNow;
}

public class CreateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public string category_name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? image_url { get; set; }
    public string? status { get; set; }

    public bool IsActive { get; set; } = true;

}


public class UpdateCategoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? category_name { get; set; }

    public string? Description { get; set; }

    public string? image_url { get; set; }
    public string? status { get; set; }

    public bool IsActive { get; set; }
}

