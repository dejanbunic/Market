using Market.Application.Models;
using Market.Application.Repository;
using Market.Application.Services;

namespace Market.Infrastructure.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IProductService _productService;

        public AttributeService(IAttributeRepository attributeRepository, IProductService productService)
        {
            _attributeRepository = attributeRepository;
            _productService = productService;
        }

        public async Task<Domain.Entity.Attribute> CreateAttributeAsync(Domain.Entity.Attribute attribute)
        {
            var product = await _productService.GetByIdAsync(attribute.ProductId);
            return await _attributeRepository.CreateAttributeAsync(attribute);
        }

        public async Task<bool> DeleteAttributeAsync(Guid id)
        {
            return await _attributeRepository.DeleteAttributeAsync(id);
        }

        public async Task<IEnumerable<Domain.Entity.Attribute>> GetAllAsync(AttributeQueryRequest attributeQueryRequest)
        {
           return await _attributeRepository.GetAllAsync(attributeQueryRequest);
        }

        public async Task<Domain.Entity.Attribute> GetByIdAsync(Guid id)
        {
            return await _attributeRepository.GetByIdAsync(id);
        }

        public async Task<Domain.Entity.Attribute> UpdateAttributeAsync(Domain.Entity.Attribute product)
        {
            return await _attributeRepository.UpdateAttributeAsync(product);
        }
    }
}
