using Api.Common.Dtos.Category;
using Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        public record UpdateStatusCategoryRequest(bool IsActive);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync([FromQuery] CategoryQueryParamsDto queryParams)
        {
            var categories = await categoryService.GetCategoriesAsync(queryParams);
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CreateCategoryDto request)
        {
            var category = await categoryService.CreateCategoryAsync(request);
            return CreatedAtRoute("GetCategoryById", new { id = category?.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryDto request)
        {
            var updatedCategory = await categoryService.UpdateCategoryAsync(id, request);
            if (!updatedCategory) return NotFound("Category not found.");
            return Ok(updatedCategory);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<bool>> UpdateStatusCategoryAsync(int id, [FromBody] UpdateStatusCategoryRequest request)
        {
            var updatedCategory = await categoryService.UpdateStatusCategoryAsync(id, request.IsActive);
            if (!updatedCategory) return NotFound("Category not found.");
            return Ok(updatedCategory);
        }
    }
}
