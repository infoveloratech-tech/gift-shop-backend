using gift_shop.DTOs;
using System.Text.RegularExpressions;

namespace gift_shop.Validators;

public static class CategoryValidator
{
    /// <summary>
    /// Validates CreateCategoryDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateCreateCategory(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.category_name))
            return (false, "Category name is required");

        if (dto.category_name.Length < 2 || dto.category_name.Length > 50)
            return (false, "Category name must be between 2 and 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
            return (false, "Category description cannot exceed 500 characters");

        if (!string.IsNullOrWhiteSpace(dto.image_url) && !IsValidUrl(dto.image_url))
            return (false, "Invalid image URL format");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates UpdateCategoryDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateUpdateCategory(UpdateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.category_name))
            return (false, "Category name is required");

        if (dto.category_name.Length < 2 || dto.category_name.Length > 50)
            return (false, "Category name must be between 2 and 50 characters");

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
            return (false, "Category description cannot exceed 500 characters");

        if (!string.IsNullOrWhiteSpace(dto.image_url) && !IsValidUrl(dto.image_url))
            return (false, "Invalid image URL format");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates category name uniqueness
    /// </summary>
    public static bool IsValidCategoryName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && 
               name.Length >= 2 && 
               name.Length <= 50 &&
               !name.Any(c => char.IsControl(c));
    }

    /// <summary>
    /// Validates URL format
    /// </summary>
    private static bool IsValidUrl(string url)
    {
        try
        {
            var uri = new Uri(url);
            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates category active status
    /// </summary>
    public static bool IsValidCategoryStatus(bool isActive)
    {
        return true; // Boolean validation is implicit
    }

    /// <summary>
    /// Validates image URL format
    /// </summary>
    public static bool IsValidImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return true; // Optional field

        return IsValidUrl(imageUrl);
    }
}
