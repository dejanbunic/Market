using Market.Application.Models;
using Attribute = Market.Domain.Entity.Attribute;

namespace Market.Application.Services
{
    public interface IAttributeService
    {
        public Task<Attribute> GetByIdAsync(Guid id);
        public Task<IEnumerable<Attribute>> GetAllAsync(AttributeQueryRequest attributeQueryRequest);

        public Task<Attribute> CreateAttributeAsync(Attribute product);
        public Task<bool> DeleteAttributeAsync(Guid id);
        public Task<Attribute> UpdateAttributeAsync(Attribute product);
    }
}
