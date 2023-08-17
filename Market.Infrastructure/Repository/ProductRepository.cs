using Market.Application.Models;
using Market.Application.Repository;
using Market.Domain;
using Market.Domain.Entity;
using Market.Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketContext _productContext;

        public ProductRepository(MarketContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            _productContext.Products.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            Product product = await this.GetByIdAsync(id);
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(ProductQueryRequest productQueryRequest)
        {
            IQueryable<Product> products = _productContext.Products.Include(p=> p.Attributes);
            if (productQueryRequest.Name is not null)
            {
                products = products.Where(x => x.Name.ToLower().StartsWith(productQueryRequest.Name.ToLower()));
            }

            if (productQueryRequest.Group is not null)
            {
                products = products.Where(x => x.Group.StartsWith(productQueryRequest.Group));
            }

            if (productQueryRequest.Attributes is not null && productQueryRequest.Attributes.Count>0)
            {
                products = products.Where(product => product.Attributes.Count(attribute => productQueryRequest.Attributes.Contains(attribute.Id)) == productQueryRequest.Attributes.Count);
            }

            products = products.Skip(productQueryRequest.Skip);

            if (productQueryRequest.Take is not null)
            {
                products = products.Take(productQueryRequest.Take.Value);
            }

            return await products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid productId)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            
            if(product == null)
                throw new ProductNotFoundException();
            return product;

        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            Product productDb = await this.GetByIdAsync(product.Id);
            productDb.Name = product.Name;
            productDb.Barcode = product.Barcode;
            productDb.MeasureUnit = product.MeasureUnit;
            productDb.Group = product.Group;
            await _productContext.SaveChangesAsync();
            return productDb;
        }
    }
}
