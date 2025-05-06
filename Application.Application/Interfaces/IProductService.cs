using TestVisibleO.Application.DTOs;
using TestVisibleO.Domain.Models;

namespace TestVisibleO.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddProductAsync(CreateProductDto dto);
        Task UpdatePriceAsync(int id, UpdatePriceDto dto);
        Task UpdateProductAsync( UpdateProductDto dto);
        Task DeleteProductAsync(int id);

    }
}
