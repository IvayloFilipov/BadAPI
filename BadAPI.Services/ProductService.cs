using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;

using static Common.GlobalConstants;

namespace BadApi.Services
{
    public class ProductService : IProductService
    {
        //private ProductRepository _repo = new ProductRepository();
        private readonly IProductRepository productRepo;
        private readonly IReviewRepository reviewRepo;

        public ProductService(IProductRepository productRepo, IReviewRepository reviewRepo)
        {
            this.productRepo = productRepo;
            this.reviewRepo = reviewRepo;
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
            await productRepo.SaveChangesAsync();

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
            {
                return Product_Not_Found;
            }

            if (product.Price > 50)
            {
                return Cannot_Delete_Product;
            }

            bool hasReviews = await reviewRepo.ProductHasReviewsAsync(id);

            if (hasReviews)
            {
                return Cannot_Delete_Product_With_Review;
            }

            await productRepo.DeleteProductAsync(id);
            await productRepo.SaveChangesAsync();

            return Deleted;
        }
    }
}