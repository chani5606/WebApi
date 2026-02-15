using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
  
{  
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICatgorySerices _categoryService;

        public CategoryController(
            ICatgorySerices categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategorieS();
            return Ok(categories);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(category);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        
        public async Task<ActionResult<CategoryResponseDto>> Create([FromBody] CategoryCreateDto createDto)
        {
            try
            {
                var category = await _categoryService.CreateCategory(createDto);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> Update(int id, [FromBody] CategoryUpdateDto updateDto)
        {
            try
            {
                var category = await _categoryService.UpdateCategory(id, updateDto);

                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (!result)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            return Ok(result);
        }

    }
}
