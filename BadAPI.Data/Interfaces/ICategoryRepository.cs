using BadAPI.Data.Entities;
using Common.DTOs;

namespace BadAPI.Data.Interfaces
{
    public interface ICategoryRepository
    {
        //Task<List<CategoryOutputDTO>> GetAllCategoriesAsync(); // to CategoryOutputDTO or to stay with the entity Category ?
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategooryByIdAsync(int id);
        Task InsertCategoryAsync(CategoryDTO category);
        Task UpdateCategoryAsync(CategoryDTO category);
        Task DeleteCategoryAsync(int id);
    }
}
