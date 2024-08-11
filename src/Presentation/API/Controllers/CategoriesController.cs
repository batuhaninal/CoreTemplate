using Application.Abstractions.Services.Categories;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Categories;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting(AppOption.RateLimiting)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.GetAllAsync(pagination));

        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] CategoryRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.GetAllAsync(parameter, pagination));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto) =>
            CreateResponse(await _categoryService.CreateAsync(createCategoryDto));


        [HttpGet("{categoryid}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "categoryid")] string categoryId) =>
            CreateResponse(await _categoryService.GetByIdAsync(categoryId));

        [HttpDelete("{categoryid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove([FromRoute(Name = "categoryid")] string categoryId) =>
            CreateResponse(await _categoryService.RemoveAsync(categoryId));

        [HttpPut("{categoryid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute(Name= "categoryid")] string categoryId, [FromBody] UpdateCategoryDto updateCategoryDto) =>
            CreateResponse(await _categoryService.UpdateAsync(categoryId, updateCategoryDto));
    }
}
