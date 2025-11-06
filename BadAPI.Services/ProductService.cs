using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;

using static Common.GlobalConstants;

namespace BadApi.Services
{
    public class ProductService : IProductService
    {
        //private ProductRepository _repo = new ProductRepository();
        private IProductRepository productRepo;

        public ProductService(IProductRepository productRepo)
        {
             this.productRepo = productRepo;
        }

        // Business rule: price must be > 0
        //public string AddProduct(Product product)
        //{
        //    if (product.Price <= 0)
        //    {
        //        return Price_Must_Be_Greater_Than_Zero;
        //    }

        //    _repo.Add(product);
        //    return Product_Added;
        //}
        public async Task<string> AddProductAsync(Product product)
        {
            if (product.Price <= 0)
            {
                return Price_Must_Be_Greater_Than_Zero;
            }

            await productRepo.AddProductAsync(product);

            return Product_Added;
        }

        //public List<Product> GetProducts()
        //{
        //    return _repo.GetAll();
        //}
        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await productRepo.GetAllProductsAsync();

            return products;
        }

        // Business rule: cannot delete product if price > 100
        //public string DeleteProduct(int id)
        //{
        //    var product = _repo.GetById(id);
        //    if (product == null)
        //        return Product_Not_Found;

        //    if (product.Price > 100)
        //        return Cannot_Delete_Expensive_Products;

        //    _repo.Delete(id);
        //    return Deleted;
        //}
        public async Task<string> DeleteProductByIdAsync(int id)
        {
            var product = await productRepo.GetProductsByIdAsync(id);

            if (product == null)
                return Product_Not_Found;

            if (product.Price > 100)
                return Cannot_Delete_Expensive_Products;

            await productRepo.DeleteProductAsync(id);

            return Deleted;
        }
    }
}