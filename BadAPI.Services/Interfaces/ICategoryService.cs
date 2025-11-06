using BadAPI.Data.Entities;

namespace BadAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<string> AddCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
    }
}
