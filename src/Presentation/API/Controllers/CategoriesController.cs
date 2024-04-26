using Application.Abstractions.Services.Categories;
using Application.Models.DTOs.Categories;
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
            return CreateResponse(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            return CreateResponse(await _categoryService.CreateAsync(createCategoryDto));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute(Name = "categoryId")] string categoryId)
        {
            return CreateResponse(await _categoryService.GetByIdAsync(categoryId));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> RemoveCategory([FromRoute(Name = "categoryId")] string categoryId) 
        {
            return CreateResponse(await _categoryService.RemoveAsync(categoryId));
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute(Name="categoryId")] string categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            return CreateResponse(await _categoryService.UpdateAsync(categoryId, updateCategoryDto));
        }
    }
}
