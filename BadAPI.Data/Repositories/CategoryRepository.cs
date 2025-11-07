using BadApi.Data;
using BadAPI.Data.Interfaces;
using Common.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BadApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BadDbContext context; // = new BadDbContext();

        public CategoryRepository(BadDbContext context)
        {
            this.context = context;
        }

        //public List<Category> GetAll()
        //{
        //    return context.Set<Category>().ToList();
        //}
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await context.Set<CategoryDTO>().ToListAsync();

            return categories;
        }

        //public Category GetById(int id)
        //{
        //    return _context.Set<Category>().FirstOrDefault(c => c.Id == id);
        //}
        public async Task<CategoryDTO> GetCategooryByIdAsync(int id)
        {
            var category = await context.Set<CategoryDTO>().FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        //public void Add(Category category)
        //{
        //    _context.Set<Category>().Add(category);
        //    _context.SaveChanges();
        //}
        public async Task InsertCategoryAsync(CategoryDTO category)
        {
            await context.Set<CategoryDTO>().AddAsync(category);
            await context.SaveChangesAsync();
        }

        //public void Update(Category category)
        //{
        //    var c = _context.Set<Category>().FirstOrDefault(x => x.Id == category.Id);
        //    if (c != null)
        //    {
        //        c.Name = category.Name;
        //        c.Description = category.Description;
        //        _context.SaveChanges();
        //    }
        //}
        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            var currCategory = await context.Set<CategoryDTO>().FirstOrDefaultAsync(x => x.Id == category.Id);

            if (currCategory != null)
            {
                currCategory.Name = category.Name;
                currCategory.Description = category.Description;

                await context.SaveChangesAsync();
            }
        }
        //public void Delete(int id)
        //{
        //    var c = _context.Set<Category>().FirstOrDefault(x => x.Id == id);
        //    if (c != null)
        //    {
        //        _context.Set<Category>().Remove(c);
        //        _context.SaveChanges();
        //    }
        //}
        public async Task DeleteCategoryAsync(int id)
        {
            var currCategory = await context.Set<CategoryDTO>().FirstOrDefaultAsync(x => x.Id == id);

            if (currCategory != null)
            {
                context.Set<CategoryDTO>().Remove(currCategory);

                await context.SaveChangesAsync();
            }
        }
    }
}