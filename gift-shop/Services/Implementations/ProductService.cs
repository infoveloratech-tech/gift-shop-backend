using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.created_at = DateTime.UtcNow;
        product.updated_at = DateTime.UtcNow;

        return await _productRepository.CreateAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var existing = await _productRepository.GetByIdAsync(product.product_id);
        if (existing == null) return false;

        product.updated_at = DateTime.UtcNow;

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existing = await _productRepository.GetByIdAsync(id);
        if (existing == null) return false;

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _productRepository.GetByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string name)
    {
        return await _productRepository.SearchAsync(name);
    }
}
