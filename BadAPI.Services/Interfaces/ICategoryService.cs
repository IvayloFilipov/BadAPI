using BadAPI.Data.Entities;
using Common.DTOs;

namespace BadAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<string> AddCategoryAsync(CategoryDTO category);
        Task<List<CategoryDTO>> GetCategoriesAsync();
    }
}
