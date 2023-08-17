using MapsterMapper;
using Market.Application.Services;
using Market.Dtos;
using Microsoft.AspNetCore.Mvc;
using Market.Domain.Entity;
using Attribute = Market.Domain.Entity.Attribute;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        private readonly IMapper _mapper;

        public AttributeController(IAttributeService attributeService, IMapper mapper)
        {
            _attributeService = attributeService;
            _mapper = mapper;
        }

        [HttpGet("attributeId")]
        public async Task<IActionResult> GetById(Guid attributeId)
        {
            var attributeResult = await _attributeService.GetByIdAsync(attributeId);
            var attribute = _mapper.Map<AttributeDto>(attributeResult);
            return Ok(attribute);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AttributeQueryRequest attribiteQueryRequest)
        {
            var queryParams = _mapper.Map<Application.Models.AttributeQueryRequest>(attribiteQueryRequest);
            var attributesResult = await _attributeService.GetAllAsync(queryParams);
            var attributes = _mapper.Map<List<AttributeDto>>(attributesResult);
            return Ok(attributes);
        }

  
        [HttpDelete("attributeId")]
        public async Task<IActionResult> Delete(Guid attributeId)
        {
            bool attributeDeleted = await _attributeService.DeleteAttributeAsync(attributeId);
            return Ok(attributeDeleted);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeDto attributePostRequest)
        {
            Domain.Entity.Attribute attribute = _mapper.Map<Domain.Entity.Attribute>(attributePostRequest);
            var createdAttributeDb = await _attributeService.CreateAttributeAsync(attribute);
            var createdAttributeDto = _mapper.Map<AttributeDto>(createdAttributeDb);
            return Ok(createdAttributeDto);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttributeDto attributeUpdateRequest)
        {
            Attribute attributeToUpdate = _mapper.Map<Attribute>(attributeUpdateRequest);
            Attribute attributeUpdated = await _attributeService.UpdateAttributeAsync(attributeToUpdate);
            var attributeResult = _mapper.Map<AttributeDto>(attributeUpdated);
            return Ok(attributeResult);
        }
    }
}
