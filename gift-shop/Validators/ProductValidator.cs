using gift_shop.DTOs;

namespace gift_shop.Validators;

public static class ProductValidator
{
    /// <summary>
    /// Validates CreateProductDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateCreateProduct(CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return (false, "Product name is required");

        if (dto.Name.Length < 3 || dto.Name.Length > 100)
            return (false, "Product name must be between 3 and 100 characters");

        if (string.IsNullOrWhiteSpace(dto.Sku))
            return (false, "SKU is required");

        if (dto.Sku.Length < 2 || dto.Sku.Length > 50)
            return (false, "SKU must be between 2 and 50 characters");

        if (dto.Price <= 0)
            return (false, "Product price must be greater than 0");

        if (dto.Cost < 0)
            return (false, "Product cost cannot be negative");

        if (dto.Cost > dto.Price)
            return (false, "Product cost cannot be greater than price");

        if (dto.CategoryId <= 0)
            return (false, "Valid category ID is required");

        if (dto.SupplierId <= 0)
            return (false, "Valid supplier ID is required");

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 1000)
            return (false, "Product description cannot exceed 1000 characters");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates UpdateProductDto
    /// </summary>
    public static (bool isValid, string errorMessage) ValidateUpdateProduct(UpdateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return (false, "Product name is required");

        if (dto.Name.Length < 3 || dto.Name.Length > 100)
            return (false, "Product name must be between 3 and 100 characters");

        if (string.IsNullOrWhiteSpace(dto.Sku))
            return (false, "SKU is required");

        if (dto.Price <= 0)
            return (false, "Product price must be greater than 0");

        if (dto.Cost < 0)
            return (false, "Product cost cannot be negative");

        if (dto.Cost > dto.Price)
            return (false, "Product cost cannot be greater than price");

        if (dto.CategoryId <= 0)
            return (false, "Valid category ID is required");

        if (dto.SupplierId <= 0)
            return (false, "Valid supplier ID is required");

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates product SKU format
    /// </summary>
    public static bool IsValidSkuFormat(string sku)
    {
        return !string.IsNullOrWhiteSpace(sku) && 
               sku.Length >= 2 && 
               sku.Length <= 50 &&
               sku.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
    }

    /// <summary>
    /// Validates product price
    /// </summary>
    public static bool IsValidProductPrice(decimal price)
    {
        return price > 0 && price <= 999999.99m;
    }

    /// <summary>
    /// Validates profit margin
    /// </summary>
    public static bool IsValidProfitMargin(decimal price, decimal cost)
    {
        return cost < price && ((price - cost) / price * 100) >= 5; // Minimum 5% margin
    }
}
