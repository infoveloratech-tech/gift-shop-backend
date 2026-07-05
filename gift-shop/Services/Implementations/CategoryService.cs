using gift_shop.Data;
using gift_shop.DTOs;
using gift_shop.Models;
using gift_shop.Repositories.Implementations;
using gift_shop.Repositories.Interfaces;
using gift_shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(x => new CategoryDto
        {
            category_id = x.category_id,
            category_name = x.category_name,
            description = x.description,
            status = x.status
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            return null;

        return new CategoryDto
        {
            category_id = category.category_id,
            category_name = category.category_name,
            description = category.description,
            status = category.status
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            category_name = dto.category_name,
            description = dto.Description,
            status = dto.status
        };

        var result = await _repository.AddAsync(category);

        return new CategoryDto
        {
            category_id = result.category_id,
            category_name = result.category_name,
            description= result.description,
            status= result.status
        };
    }

    public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(dto.Id);

        if (category == null)
            return false;

        category.category_name = dto.category_name;
        category.description = dto.Description;
        category.status = dto.status;

        await _repository.UpdateAsync(category);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            return false;

        await _repository.DeleteAsync(category);

        return true;
    }


}
