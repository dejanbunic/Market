using Market.Application.Models;
using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
