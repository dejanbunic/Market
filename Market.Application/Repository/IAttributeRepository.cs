using Market.Application.Models;
using Attribute = Market.Domain.Entity.Attribute;

namespace Market.Application.Repository
{
    public interface IAttributeRepository
    {
        Task<IEnumerable<Attribute>> GetAllAsync(AttributeQueryRequest attributeQueryRequest);
        Task<Attribute> GetByIdAsync(Guid attributeId);
        Task<Attribute> CreateAttributeAsync(Attribute attribute);
        Task<Attribute> UpdateAttributeAsync(Attribute attribute);
        Task<bool> DeleteAttributeAsync(Guid attributeId);
    }
}
