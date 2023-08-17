using Market.Application.Models;
using Market.Domain.Entity;

namespace Market.Application.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync(ProductQueryRequest productQueryRequest);

        Task<Product> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<Product> UpdateProductAsync(Product product);
    }
}
