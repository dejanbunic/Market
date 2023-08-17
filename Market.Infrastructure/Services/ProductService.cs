using Market.Application.Models;
using Market.Application.Repository;
using Market.Application.Services;
using Market.Domain.Entity;

namespace Market.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> CreateProductAsync(Product product)
        {
            return _productRepository.CreateProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(Guid id)
        {
            return _productRepository.DeleteProductAsync(id);
        }

        public Task<IEnumerable<Product>> GetAllAsync(ProductQueryRequest productQueryRequest)
        {
           return _productRepository.GetAllAsync(productQueryRequest);
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return _productRepository.GetByIdAsync(id);
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            return _productRepository.UpdateProductAsync(product);
        }
    }
}
