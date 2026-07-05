using gift_shop.DTOs;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {


            private readonly ICategoryService _categoryService;

            public CategoriesController(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            // GET: api/categories
            [HttpGet]
        [AllowAnonymous]
            public async Task<IActionResult> GetAll()
            {
            var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }

            // GET: api/categories/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Category not found."
                    });

                return Ok(category);
            }

            // POST: api/categories
            [AllowAnonymous]
            [HttpPost]
            public async Task<IActionResult> Create(CreateCategoryDto dto)
            {
                var result = await _categoryService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = result.category_id },
                    result);
            }

            // PUT: api/categories/5
            [Authorize(Roles = "Admin")]
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
            {
                if (id != dto.Id)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Category ID mismatch."
                    });

                var updated = await _categoryService.UpdateAsync(dto);

                if (!updated)
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Category not found."
                    });

                return Ok(new
                {
                    Success = true,
                    Message = "Category updated successfully."
                });
            }

            // DELETE: api/categories/5
            [Authorize(Roles = "Admin")]
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var deleted = await _categoryService.DeleteAsync(id);

                if (!deleted)
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Category not found."
                    });

                return Ok(new
                {
                    Success = true,
                    Message = "Category deleted successfully."
                });
            }
        }
    
}
