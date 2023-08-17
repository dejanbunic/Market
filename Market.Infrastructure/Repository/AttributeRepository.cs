﻿using Market.Application.Models;
using Market.Application.Repository;
using Market.Domain;
using Market.Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly MarketContext _attributeContext;

        public AttributeRepository(MarketContext attributeContext)
        {
            _attributeContext = attributeContext;
        }


        public async Task<Domain.Entity.Attribute> CreateAttributeAsync(Domain.Entity.Attribute attribute)
        {
            attribute.Id = Guid.NewGuid();
            _attributeContext.Attributes.Add(attribute);
            await _attributeContext.SaveChangesAsync();
            return attribute;
        }

        public async Task<bool> DeleteAttributeAsync(Guid attributeId)
        {
            Domain.Entity.Attribute attribute = await this.GetByIdAsync(attributeId);
            _attributeContext.Attributes.Remove(attribute);
            await _attributeContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Domain.Entity.Attribute>> GetAllAsync(AttributeQueryRequest attributeQueryRequest)
        {
            var attributes = _attributeContext.Attributes;
            return await attributes.ToListAsync();
        }

        public async Task<Domain.Entity.Attribute> GetByIdAsync(Guid attributeId)
        {

            var attribute = await _attributeContext.Attributes.FirstOrDefaultAsync(x => x.Id == attributeId);
            if (attribute == null)
                throw new AttributeNotFoundException();
            
            return attribute;
        }

        public async Task<Domain.Entity.Attribute> UpdateAttributeAsync(Domain.Entity.Attribute attribute)
        {
            Domain.Entity.Attribute attributeDb = await this.GetByIdAsync(attribute.Id);
            attributeDb.Name = attribute.Name;
            attributeDb.Value = attribute.Value;
            await _attributeContext.SaveChangesAsync();
            return attributeDb;
        }
    }
}
