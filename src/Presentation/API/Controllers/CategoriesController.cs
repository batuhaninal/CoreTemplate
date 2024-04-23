using Application.Abstractions.Commons.Results;
using Application.Abstractions.Services.Categories;
using Application.Models.DTOs.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return CreateResponse<IPaginatedDataResult<CategoryItemDto>>(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromBody] CreateCategoryDto createCategoryDto)
        {
            return CreateResponse<IBaseResult>(await _categoryService.CreateAsync(createCategoryDto));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "categoryId")] string categoryId)
        {
            return CreateResponse<IDataResult<CategoryItemDto>>(await _categoryService.GetByIdAsync(categoryId));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> RemoveCategory([FromRoute(Name = "categoryId")] string categoryId) 
        {
            return CreateResponse<IBaseResult>(await _categoryService.RemoveAsync(categoryId));
        }
    }
}
