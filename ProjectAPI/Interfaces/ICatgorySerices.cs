using ProjectAPI.DTOs;

namespace ProjectAPI.Interfaces
{
    public interface ICatgorySerices
    {
        Task<CategoryResponseDto> CreateCategory(CategoryCreateDto createDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryResponseDto>> GetAllCategorieS();
        Task<CategoryResponseDto?> GetCategoryById(int id);
        Task<CategoryResponseDto?> UpdateCategory(int id, CategoryUpdateDto updateDto);
    }
}