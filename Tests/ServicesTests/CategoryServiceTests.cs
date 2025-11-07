using BadApi.Services;
using BadAPI.Data.Interfaces;
using Common.DTOs;
using Moq;

namespace Tests.ServicesTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> mockCategoryRepo;
        private CategoryService categoryService;

        private const string Category_Name_Is_Required = "Category name is required";
        private const string Category_Added = "Category added";

        [SetUp]
        public void Setup()
        {
            mockCategoryRepo = new Mock<ICategoryRepository>();
            categoryService = new CategoryService(mockCategoryRepo.Object);
        }

        [Test]
        public async Task AddCategoryAsync_ReturnsError_WhenNameIsNull()
        {
            // Arrange
            var category = new CategoryDTO { Name = null };

            // Act
            var result = await categoryService.AddCategoryAsync(category);

            // Assert
            Assert.AreEqual(Category_Name_Is_Required, result);
            mockCategoryRepo.Verify(r => r.InsertCategoryAsync(It.IsAny<CategoryDTO>()), Times.Never);
        }

        [Test]
        public async Task AddCategoryAsync_ReturnsError_WhenNameIsEmpty()
        {
            // Arrange
            var category = new CategoryDTO { Name = "" };

            // Act
            var result = await categoryService.AddCategoryAsync(category);

            // Assert
            Assert.AreEqual(Category_Name_Is_Required, result);
            mockCategoryRepo.Verify(r => r.InsertCategoryAsync(It.IsAny<CategoryDTO>()), Times.Never);
        }

        [Test]
        public async Task AddCategoryAsync_InsertsCategory_WhenValid()
        {
            // Arrange
            var category = new CategoryDTO { Name = "Books" };

            mockCategoryRepo
                .Setup(r => r.InsertCategoryAsync(category))
                .Returns(Task.CompletedTask);

            // Act
            var result = await categoryService.AddCategoryAsync(category);

            // Assert
            Assert.AreEqual(Category_Added, result);
            mockCategoryRepo.Verify(r => r.InsertCategoryAsync(category), Times.Once);
        }

        [Test]
        public async Task GetCategoriesAsync_ReturnsCategoryList()
        {
            // Arrange
            var expected = new List<CategoryDTO>
        {
            new CategoryDTO { Id = 1, Name = "Test1" },
            new CategoryDTO { Id = 2, Name = "Test2" }
        };

            mockCategoryRepo
                .Setup(r => r.GetAllCategoriesAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await categoryService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test1", result[0].Name);
        }

        [Test]
        public async Task GetCategoriesAsync_ReturnsEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            mockCategoryRepo
                .Setup(r => r.GetAllCategoriesAsync())
                .ReturnsAsync(new List<CategoryDTO>());

            // Act
            var result = await categoryService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetCategoriesAsync_PropagatesException()
        {
            // Arrange
            mockCategoryRepo
                .Setup(r => r.GetAllCategoriesAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await categoryService.GetCategoriesAsync();
            });
        }
    }
}
