using Application.Application.DTOs;
using TestVisibleO.Domain.Models;

namespace Application.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(CreateProductDto dto);
        Task UpdatePriceAsync(int id, UpdatePriceDto dto);
        Task DeleteProductAsync(int id);
    }
}
