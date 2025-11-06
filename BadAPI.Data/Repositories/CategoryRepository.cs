using BadApi.Data;
using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
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
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await context.Set<Category>().ToListAsync();

            return categories;
        }

        //public Category GetById(int id)
        //{
        //    return _context.Set<Category>().FirstOrDefault(c => c.Id == id);
        //}
        public async Task<Category> GetCategooryByIdAsync(int id)
        {
            var category = await context.Set<Category>().FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        //public void Add(Category category)
        //{
        //    _context.Set<Category>().Add(category);
        //    _context.SaveChanges();
        //}
        public async Task InsertCategoryAsync(Category category)
        {
            await context.Set<Category>().AddAsync(category);
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
        public async Task UpdateCategoryAsync(Category category)
        {
            var currCategory = await context.Set<Category>().FirstOrDefaultAsync(x => x.Id == category.Id);

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
            var currCategory = await context.Set<Category>().FirstOrDefaultAsync(x => x.Id == id);

            if (currCategory != null)
            {
                context.Set<Category>().Remove(currCategory);

                await context.SaveChangesAsync();
            }
        }
    }
}