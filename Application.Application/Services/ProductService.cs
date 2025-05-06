using TestVisibleO.Application.DTOs;
using TestVisibleO.Application.Interfaces;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Domain.Models;

namespace TestVisibleO.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddProductAsync(CreateProductDto dto)
        {
            if (dto.Price <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            var product = new Product
            {
                NameProduct = dto.Name,
                DescriptionProduct = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl
            };

            await _repository.AddAsync(product);
        }

        public async Task UpdatePriceAsync(int id, UpdatePriceDto dto)
        {
            if (dto.Price <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            product.Price = dto.Price;
            product.DiscountPrice = dto.DiscountPrice;

            await _repository.UpdateAsync(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(dto.Id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            product.NameProduct = dto.Name;
            product.DescriptionProduct = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
