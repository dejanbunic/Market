using MapsterMapper;
using Market.Application.Services;
using Market.Domain.Entity;
using Market.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace Market.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("productId")]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var productResult = await _productService.GetByIdAsync(productId);
            var product = _mapper.Map<ProductDto>(productResult);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryRequest productQueryRequest)
        {
            var queryParams = _mapper.Map<Application.Models.ProductQueryRequest>(productQueryRequest);
            var productsResult = await _productService.GetAllAsync(queryParams);
            var products = _mapper.Map<List<ProductDto>>(productsResult);
            return Ok(products);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId)
        {
            bool result = await _productService.DeleteProductAsync(productId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productPostRequest)
        {
            Product product = _mapper.Map<Product>(productPostRequest);
            var createdProductDb = await _productService.CreateProductAsync(product);
            var createdProductDto = _mapper.Map<ProductDto>(createdProductDb);
            return Ok(createdProductDto);
        }
        [HttpPut]

        public async Task<IActionResult> Update([FromBody] ProductDto productUpdateRequest)
        {
            Product productToUpdate = _mapper.Map<Product>(productUpdateRequest);
            Product productUpdated = await _productService.UpdateProductAsync(productToUpdate);
            var productResult = _mapper.Map<ProductDto>(productUpdated);
            return Ok(productResult);
        }
    }
}
