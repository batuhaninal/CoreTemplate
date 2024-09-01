using Application.Abstractions.Services.Categories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(1)]
    public class TestsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public TestsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }
    }
}
