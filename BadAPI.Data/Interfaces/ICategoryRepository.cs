using BadAPI.Data.Entities;

namespace BadAPI.Data.Interfaces
{
    public interface ICategoryRepository
    {
        //Task<List<CategoryOutputDTO>> GetAllCategoriesAsync(); // to CategoryOutputDTO or to stay with the entity Category ?
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategooryByIdAsync(int id);
        Task InsertCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
