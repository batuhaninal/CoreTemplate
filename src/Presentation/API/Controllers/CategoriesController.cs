﻿using Application.Abstractions.Services.Categories;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Categories;
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
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            return CreateResponse(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveCategory([FromRoute(Name = "categoryId")] string categoryId) 
        {
            return CreateResponse(await _categoryService.RemoveAsync(categoryId));
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute(Name="categoryId")] string categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            return CreateResponse(await _categoryService.UpdateAsync(categoryId, updateCategoryDto));
        }
    }
}
