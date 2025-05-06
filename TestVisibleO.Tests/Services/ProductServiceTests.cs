
using FluentAssertions;
using Moq;
using TestVisibleO.Application.DTOs;
using TestVisibleO.Application.Services;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Domain.Models;

namespace TestVisibleO.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct_WhenValidInput()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                Name = "Laptop",
                Description = "Gaming Laptop",
                Price = 1200.99m,
                ImageUrl = "http://example.com/laptop.png"
            };

            // Act
            await _service.AddProductAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p =>
                p.NameProduct == dto.Name &&
                p.DescriptionProduct == dto.Description &&
                p.Price == dto.Price &&
                p.ImageUrl == dto.ImageUrl
            )), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task AddProductAsync_ShouldThrow_WhenPriceIsZeroOrNegative(decimal price)
        {
            // Arrange
            var dto = new CreateProductDto
            {
                Name = "Mouse",
                Description = "Wireless Mouse",
                Price = price
            };

            // Act
            Func<Task> act = async () => await _service.AddProductAsync(dto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("El precio debe ser mayor que cero.");
        }

        [Fact]
        public async Task UpdatePriceAsync_ShouldUpdate_WhenProductExists()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 1,
                NameProduct = "Teclado",
                Price = 300m
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProduct);

            var dto = new UpdatePriceDto
            {
                Price = 250m,
                DiscountPrice = 200m
            };

            // Act
            await _service.UpdatePriceAsync(1, dto);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Product>(p =>
                p.Price == 250m &&
                p.DiscountPrice == 200m
            )), Times.Once);
        }

        [Fact]
        public async Task UpdatePriceAsync_ShouldThrow_WhenProductNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Product)null);

            var dto = new UpdatePriceDto
            {
                Price = 100
            };

            // Act
            Func<Task> act = async () => await _service.UpdatePriceAsync(99, dto);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Producto no encontrado.");
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallDelete_WhenExists()
        {
            // Arrange
            var product = new Product { Id = 2, NameProduct = "Tablet" };
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(product);

            // Act
            await _service.DeleteProductAsync(2);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(product.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldThrow_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync((Product)null);

            // Act
            Func<Task> act = async () => await _service.DeleteProductAsync(5);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Producto no encontrado.");
        }
    }
}
