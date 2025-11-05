using System.Collections.Generic;
using BadApi.Data;
using BadApi.Repositories;
using BadAPI.Data.Entities;
using System.Text;
using System.Threading;
using System.Runtime;
using System.Security;
using System.Timers;

using static Common.GlobalConstants;

namespace BadApi.Services
{
    public class ProductService
    {
        private ProductRepository _repo = new ProductRepository();

        // Business rule: price must be > 0
        public string AddProduct(Product product)
        {
            if (product.Price <= 0)
            {
                return Price_Must_Be_Greater_Than_Zero;
            }

            _repo.Add(product);
            return Product_Added;
        }

        public List<Product> GetProducts()
        {
            return _repo.GetAll();
        }

        // Business rule: cannot delete product if price > 100
        public string DeleteProduct(int id)
        {
            var product = _repo.GetById(id);
            if (product == null)
                return Product_Not_Found;

            if (product.Price > 100)
                return Cannot_Delete_Expensive_Products;

            _repo.Delete(id);
            return Deleted;
        }
    }
}