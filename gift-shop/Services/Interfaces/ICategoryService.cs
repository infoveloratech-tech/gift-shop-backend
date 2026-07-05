using gift_shop.DTOs;

namespace gift_shop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

        Task<bool> UpdateAsync(UpdateCategoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}