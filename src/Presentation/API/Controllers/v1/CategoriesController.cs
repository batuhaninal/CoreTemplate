using Application.Abstractions.Services.Categories;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Categories;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Categories;
using Application.Models.RequestParameters.Commons;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting(AppOption.RateLimiting)]
    [ApiVersion(1)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string condition, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.SearchAsync(condition, pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.CategoryTag])]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.GetAllAsync(pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.CategoryTag])]
        public async Task<IActionResult> GetAllBaseCategories([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.GetAllBaseCategoriesAsync(pagination.PageIndex, pagination.PageSize));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.CategoryTag])]
        public async Task<IActionResult> GetAllFiltered([FromQuery] CategoryRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _categoryService.GetAllAsync(parameter, pagination));

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto) =>
            CreateResponse(await _categoryService.CreateAsync(createCategoryDto));


        [MapToApiVersion(1)]
        [HttpGet("{categoryid}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "categoryid")] Guid categoryId) =>
            CreateResponse(await _categoryService.GetByIdAsync(categoryId));

        [MapToApiVersion(1)]
        [HttpDelete("{categoryid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove([FromRoute(Name = "categoryid")] Guid categoryId) =>
            CreateResponse(await _categoryService.RemoveAsync(categoryId));

        [MapToApiVersion(1)]
        [HttpPut("{categoryid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute(Name = "categoryid")] Guid categoryId, [FromBody] UpdateCategoryDto updateCategoryDto) =>
            CreateResponse(await _categoryService.UpdateAsync(categoryId, updateCategoryDto));
    }
}
