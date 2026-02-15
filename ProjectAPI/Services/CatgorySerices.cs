using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Services
{
    public class CatgorySerices : ICatgorySerices
    {
        private readonly ICatgoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatgorySerices> _logger;

        public CatgorySerices(
            ICatgoryRepository categoryRepository,
            IMapper mapper,
            ILogger<CatgorySerices> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategorieS()
        {
            _logger.LogInformation("Retrieving all categories");
            var categories = await _categoryRepository.GetAllCatgories();
            _logger.LogInformation("Retrieved {Count} categories", categories.Count());
            return categories.Select(c => _mapper.Map<CategoryResponseDto>(c));
        }

        public async Task<CategoryResponseDto?> GetCategoryById(int id)
        {
            _logger.LogInformation("Retrieving category with ID {Id}", id);
            var category = await _categoryRepository.GetByICatgory(id);

            if (category == null)
                _logger.LogWarning("Category with ID {Id} not found", id);

            return category != null ? _mapper.Map<CategoryResponseDto>(category) : null;
        }

        public async Task<CategoryResponseDto> CreateCategory(CategoryCreateDto createDto)
        {
            _logger.LogInformation("Creating new category with Name {Name}", createDto.Name);
            var category = _mapper.Map<Catgories>(createDto);
            var createdCategory = await _categoryRepository.CreateCatgory(category);
            _logger.LogInformation("Category created with ID {Id}", createdCategory.Id);
            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<CategoryResponseDto?> UpdateCategory(int id, CategoryUpdateDto updateDto)
        {
            _logger.LogInformation("Updating category with ID {Id}", id);
            var existingCategory = await _categoryRepository.GetByICatgory(id);

            if (existingCategory == null)
            {
                _logger.LogWarning("Category with ID {Id} not found", id);
                return null;
            }

            if (updateDto.Name != null)
            {
                existingCategory.Name = updateDto.Name;
                _logger.LogInformation("Updated category name to {Name}", updateDto.Name);
            }

            var updatedCategory = await _categoryRepository.UpdateCatgory(existingCategory);
            return updatedCategory != null ? _mapper.Map<CategoryResponseDto>(updatedCategory) : null;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            _logger.LogInformation("Deleting category with ID {Id}", id);
            var result = await _categoryRepository.DeleteCatgory(id);

            if (result)
                _logger.LogInformation("Category with ID {Id} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete category with ID {Id}", id);

            return result;
        }
    }
}
