using Microsoft.EntityFrameworkCore;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Domain.Models;
using TestVisibleO.Infrastructure.Data;

namespace TestVisibleO.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestProductsContext _context;

        public ProductRepository(TestProductsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .FromSqlRaw("EXEC SP_GetAllProducts")
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
