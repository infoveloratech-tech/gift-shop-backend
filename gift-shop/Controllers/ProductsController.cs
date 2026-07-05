using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // =========================
        // GET: api/products
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // =========================
        // GET: api/products/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // =========================
        // POST: api/products
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _productService.CreateProductAsync(product);

            return Ok(new
            {
                message = "Product created successfully",
                data = createdProduct
            });
        }

        // =========================
        // PUT: api/products/{id}
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.product_id)
                return BadRequest(new { message = "Product ID mismatch" });

            var updated = await _productService.UpdateProductAsync(product);

            if (!updated)
                return NotFound(new { message = "Product not found" });

            return Ok(new { message = "Product updated successfully" });
        }

        // =========================
        // DELETE: api/products/{id}
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
                return NotFound(new { message = "Product not found" });

            return Ok(new { message = "Product deleted successfully" });
        }

        // =========================
        // GET: api/products/category/{categoryId}
        // =========================
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        // =========================
        // GET: api/products/search?name=gift
        // =========================
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            var products = await _productService.SearchProductsAsync(name);
            return Ok(products);
        }
    }
}
