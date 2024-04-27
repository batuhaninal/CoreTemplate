using Application.Abstractions.Services.Products;
using Application.Models.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int pageIndex, [FromQuery] int pageSize) =>
            CreateResponse(await _productService.GetAllAsync(pageIndex, pageSize));

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute(Name = "productId")] string productId) => 
            CreateResponse(await _productService.GetByIdAsync(productId));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto) =>
            CreateResponse(await _productService.CreateAsync(createProductDto));

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute(Name = "productId")] string productId, [FromBody] UpdateProductDto updateProductDto) =>
            CreateResponse(await _productService.UpdateAsync(productId, updateProductDto));

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProduct([FromRoute(Name = "productId")] string productId) =>
            CreateResponse(await _productService.RemoveAsync(productId));
    }
}
