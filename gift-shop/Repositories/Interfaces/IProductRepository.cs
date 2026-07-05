using gift_shop.Models;

namespace gift_shop.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<bool> SkuExistsAsync(string sku);
    Task<int> GetTotalProductCountAsync();
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
}
