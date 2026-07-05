using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto?> GetProductBySkuAsync(string sku);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> DeactivateProductAsync(int id);
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}
