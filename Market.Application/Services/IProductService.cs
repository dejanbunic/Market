using Market.Application.Models;
using Market.Domain.Entity;

namespace Market.Application.Services
{
    public interface IProductService
    {
        public Task<Product> GetByIdAsync(Guid id);
        public Task<IEnumerable<Product>> GetAllAsync(ProductQueryRequest productQueryRequest);
        public Task<Product> CreateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(Guid id);
        public Task<Product> UpdateProductAsync(Product product);
    }
}
