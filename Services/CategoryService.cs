using Api.Common.Dtos.Category;
using Api.Common.Interfaces;
using Api.Data;
using Api.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class CategoryService(ApplicationDbContext context) : ICategoryService
    {
        public async Task<IEnumerable<CategoryDto?>> GetCategoriesAsync(CategoryQueryParamsDto queryParams)
        {
            IQueryable<Category> categories = context.Categories;

            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                categories = categories.Where(c => c.Name.Contains(queryParams.SearchTerm));
            }

            var skip = (queryParams.PageNumber - 1) * queryParams.PageSize;

            var result = await categories
                .Skip(skip)
                .Take(queryParams.PageSize)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return result;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            if (!await context.Categories.AnyAsync(c => c.Id == id))
            {
                return null;
            }

            var category = await context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto request)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return false;

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStatusCategoryAsync(int id, bool isActive)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return false;

            category.IsActive = isActive;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
