using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly GiftShopDbContext _context;

    public ProductService(GiftShopDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductDto?> GetProductBySkuAsync(string sku)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
        return product == null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
    {
        var products = await _context.Products.Where(p => p.IsActive).ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId)
    {
        var products = await _context.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            Cost = createProductDto.Cost,
            CategoryId = createProductDto.CategoryId,
            SupplierId = createProductDto.SupplierId,
            Sku = createProductDto.Sku,
            ImageUrl = createProductDto.ImageUrl,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) throw new ArgumentException("Product not found");

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.Cost = updateProductDto.Cost;
        product.CategoryId = updateProductDto.CategoryId;
        product.SupplierId = updateProductDto.SupplierId;
        product.Sku = updateProductDto.Sku;
        product.ImageUrl = updateProductDto.ImageUrl;
        product.IsActive = updateProductDto.IsActive;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await _context.Products
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm) || p.Sku.Contains(searchTerm))
            .ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var products = await _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Cost = product.Cost,
            CategoryId = product.CategoryId,
            SupplierId = product.SupplierId,
            Sku = product.Sku,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt
        };
    }
}
