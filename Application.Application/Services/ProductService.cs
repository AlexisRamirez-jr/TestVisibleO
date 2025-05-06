using Application.Application.DTOs;
using Application.Application.Interfaces;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Domain.Models;

namespace Application.Application.Services
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

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            await _repository.DeleteAsync(product.Id);
        }

    }
}
