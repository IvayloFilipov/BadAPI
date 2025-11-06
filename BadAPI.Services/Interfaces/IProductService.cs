using BadAPI.Data.Entities;

namespace BadAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<string> AddProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();
        Task<string> DeleteProductByIdAsync(int id);
    }
}
