using Api.Common.Dtos.Category;

namespace Api.Common.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto?>> GetCategoriesAsync(CategoryQueryParamsDto queryParams);
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto request);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto request);
        Task<bool> UpdateStatusCategoryAsync(int id, bool isActive);

        //Task<bool> DeleteCategoryAsync(int id);
    }
}
