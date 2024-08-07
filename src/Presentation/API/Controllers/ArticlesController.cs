﻿using Application.Abstractions.Services.Articles;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting(AppOption.RateLimiting)]
    public class ArticlesController : BaseController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllArticles([FromQuery] RichPaginationRequestParameter parameter) =>
        //    CreateResponse(await _articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize));

        [HttpGet]
        public async Task<IActionResult> GetAllArticles([FromQuery] PaginationRequestParameter parameter) =>
            CreateResponse(await _articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize));

        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetArticleById([FromRoute(Name = "articleId")] string articleId) => 
            CreateResponse(await _articleService.GetByIdAsync(articleId));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto createArticleDto) =>
            CreateResponse(await _articleService.CreateAsync(createArticleDto));

        [HttpPut("{articleId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateArticle([FromRoute(Name = "articleId")] string articleId, [FromBody] UpdateArticleDto updateArticleDto) =>
            CreateResponse(await _articleService.UpdateAsync(articleId, updateArticleDto));

        [HttpDelete("{articleId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveArticle([FromRoute(Name = "articleId")] string articleId) =>
            CreateResponse(await _articleService.RemoveAsync(articleId));
    }
}
