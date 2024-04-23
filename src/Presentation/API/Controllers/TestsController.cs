using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public TestsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }
    }
}
