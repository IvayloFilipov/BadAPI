using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

using static Common.GlobalConstants;

namespace BadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //private ProductService _service = new ProductService();
        //private ProductRepository _repo = new ProductRepository();
        private readonly IProductService productService;
        private readonly IProductRepository productRepo;

        public ProductsController(IProductService productService, IProductRepository productRepo)
        {
            this.productService = productService;
            this.productRepo = productRepo;
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    var products = _service.GetProducts();
        //    return Ok(products);
        //}
        [HttpGet("getproducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await productService.GetProductsAsync();

                return Ok(products);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{
        //    var product = _repo.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    if (product.Price > 50)
        //    {
        //        return Ok(new { product.Id, product.Name, Discount = Ten_Percent });
        //    }

        //    return Ok(product);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                var product = await productRepo.GetProductsByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                if (product.Price > 50)
                {
                    return Ok(new { product.Id, product.Name, Discount = Ten_Percent });
                }

                return Ok(product);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost]
        //public ActionResult Post(Product p)
        //{
        //    var result = _service.AddProduct(p);
        //    if (result == Price_Must_Be_Greater_Than_Zero)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                var result = await productService.AddProductAsync(product);

                if (result == Price_Must_Be_Greater_Than_Zero)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public ActionResult Put(int id, Product p)
        //{
        //    if (id != p.Id)
        //        return BadRequest(Id_Mismatch);

        //    if (p.Price <= 0)
        //        return BadRequest(Invalid_Price);

        //    var existingProduct = _repo.GetById(id);
        //    if (existingProduct == null)
        //        return NotFound();

        //    existingProduct.Name = p.Name;
        //    existingProduct.Price = p.Price;
        //    existingProduct.CategoryId = p.CategoryId;
        //    existingProduct.CategoryName = p.CategoryName;

        //    _repo.Update(existingProduct);

        //    return Ok(Updated);
        //}
        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest(Id_Mismatch);

                if (product.Price <= 0)
                    return BadRequest(Invalid_Price);

                var existingProduct = await productRepo.GetProductsByIdAsync(id);

                if (existingProduct == null)
                    return NotFound();

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.CategoryName = product.CategoryName;

                await productRepo.UpdateProductAsync(existingProduct);

                return Ok(Updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    var result = _service.DeleteProduct(id);

        //    if (result == Product_Not_Found)
        //        return NotFound(result);

        //    if (result == Cannot_Delete_Expensive_Products)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await productService.DeleteProductByIdAsync(id);

                if (result == Product_Not_Found)
                    return NotFound(result);

                if (result == Cannot_Delete_Expensive_Products)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
