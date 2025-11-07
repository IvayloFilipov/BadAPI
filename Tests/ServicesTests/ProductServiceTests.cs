using BadApi.Services;
using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;
using Moq;

namespace Tests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests1
    {
        private Mock<IProductRepository> mockProductRepo;
        private Mock<IReviewRepository> mockReviewRepo;
        private IProductService productService;

        private const string Price_Must_Be_Greater_Than_Zero = "Price must be greater than zero";
        private const string Product_Added = "Product added";

        [SetUp]
        public void Setup()
        {
            mockProductRepo = new Mock<IProductRepository>();
            mockReviewRepo = new Mock<IReviewRepository>();
            productService = new ProductService(mockProductRepo.Object, mockReviewRepo.Object);
        }

        [Test]
        public async Task AddProductAsync_ReturnsError_WhenPriceIsZero()
        {
            // Arrange
            var product = new Product { Price = 0 };

            // Act
            var result = await productService.AddProductAsync(product);

            // Assert
            Assert.AreEqual(Price_Must_Be_Greater_Than_Zero, result);
            mockProductRepo.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Never);
            mockProductRepo.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task AddProductAsync_ReturnsError_WhenPriceIsNegative()
        {
            // Arrange
            var product = new Product { Price = -5 };

            // Act
            var result = await productService.AddProductAsync(product);

            // Assert
            Assert.AreEqual(Price_Must_Be_Greater_Than_Zero, result);
            mockProductRepo.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Never);
            mockProductRepo.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task AddProductAsync_AddsProduct_WhenValid()
        {
            // Arrange
            var product = new Product { Price = 10 };

            mockProductRepo.Setup(r => r.AddProductAsync(product))
                     .Returns(Task.CompletedTask);

            mockProductRepo.Setup(r => r.SaveChangesAsync())
                     .Returns(Task.CompletedTask);

            // Act
            var result = await productService.AddProductAsync(product);

            // Assert
            Assert.AreEqual(Product_Added, result);
            mockProductRepo.Verify(r => r.AddProductAsync(product), Times.Once);
            mockProductRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            mockProductRepo
                .Setup(r => r.GetAllProductsAsync())
                .ReturnsAsync(new List<Product>
                {
                new Product { Id = 1 },
                new Product { Id = 2 }
                });

            //Act
            var result = await productService.GetProductsAsync();

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteProductByIdAsync_ShouldBlockDeletion_WhenPriceAbove50()
        {
            //Arrange
            var product = new Product { Id = 1, Price = 60 };

            //Act
            mockProductRepo.Setup(r => r.GetProductsByIdAsync(1)).ReturnsAsync(product);

            var result = await productService.DeleteProductByIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo("Products priced over $50 cannot be deleted."));
        }

        [Test]
        public async Task DeleteProductByIdAsync_ShouldBlockDeletion_WhenProductHasReviews()
        {
            //Arange
            var product = new Product { Id = 1, Price = 20 };

            mockProductRepo.Setup(r => r.GetProductsByIdAsync(1)).ReturnsAsync(product);
            mockReviewRepo.Setup(r => r.ProductHasReviewsAsync(1)).ReturnsAsync(true);

            //Act
            var result = await productService.DeleteProductByIdAsync(1);

            //Assert
            Assert.That(result, Is.EqualTo("Product cannot be deleted because it has customer reviews."));
        }
    }
}