using BadApi.Data;
using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BadApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //private BadDbContext _context = new BadDbContext();
        private readonly BadDbContext context;

        public ProductRepository(BadDbContext context)
        {
            this.context = context;
        }

        //public List<Product> GetAll()
        //{
        //    return _context.Products.ToList();
        //}
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        //public Product GetById(int id)
        //{
        //    return _context.Products.FirstOrDefault(x => x.Id == id);
        //}

        //public Product GetById(int id)
        //{
        //    return _context.Products.FirstOrDefault(x => x.Id == id);
        //}
        public async Task<Product> GetProductsByIdAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        //public void Add(Product product)
        //{
        //    _context.Products.Add(product);
        //    _context.SaveChanges();
        //}
        public async Task AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        //public void Update(Product product)
        //{
        //    var p = _context.Products.FirstOrDefault(x => x.Id == product.Id);
        //    if (p != null)
        //    {
        //        p.Name = product.Name;
        //        p.Price = product.Price;

        //        p.CategoryId = product.CategoryId;
        //        p.CategoryName = product.CategoryName;

        //        _context.SaveChanges();
        //    }
        //}
        public async Task UpdateProductAsync(Product product)
        {
            var currProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (currProduct != null)
            {
                currProduct.Name = product.Name;
                currProduct.Price = product.Price;
                
                currProduct.CategoryId = product.CategoryId;
                currProduct.CategoryName = product.CategoryName;

                context.SaveChangesAsync();
            }
        }

        //public void Delete(int id)
        //{
        //    var p = _context.Products.FirstOrDefault(x => x.Id == id);
        //    if (p != null)
        //    {
        //        _context.Products.Remove(p);
        //        _context.SaveChanges();
        //    }
        //}
        public async Task DeleteProductAsync(int id)
        {
            var currProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (currProduct != null)
            {
                context.Products.Remove(currProduct);

                await context.SaveChangesAsync();
            }
        }
    }
}