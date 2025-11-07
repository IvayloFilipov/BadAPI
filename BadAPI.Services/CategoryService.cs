using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;
using Common.DTOs;

using static Common.GlobalConstants;

namespace BadApi.Services
{
    public class CategoryService : ICategoryService
    {
        //private CategoryRepository _repo = new CategoryRepository();
        private readonly ICategoryRepository categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
                this.categoryRepo = categoryRepo;
        }

        // Business rule: Name cannot be empty
        //public string AddCategory(Category category)
        //{
        //    if (string.IsNullOrEmpty(category.Name))
        //    {
        //        return Category_Name_Is_Required;
        //    }

        //    _repo.Add(category);
        //    return Category_Added;
        //}
        public async Task<string> AddCategoryAsync(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                return Category_Name_Is_Required;
            }

            await categoryRepo.InsertCategoryAsync(category);

            return Category_Added;
        }

        //public List<Category> GetCategories()
        //{
        //    return _repo.GetAll();
        //}
        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            return await categoryRepo.GetAllCategoriesAsync();
        }
    }
}
