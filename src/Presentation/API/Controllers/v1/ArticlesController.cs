using Application.Abstractions.Services.Articles;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
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

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.ArticleTag])]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter parameter) =>
            CreateResponse(await _articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize));

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string condition, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _articleService.SearchAsync(condition, pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.ArticleTag])]
        public async Task<IActionResult> GetAllFiltered([FromQuery] ArticleRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _articleService.GetAllAsync(parameter, pagination));

        [MapToApiVersion(1)]
        [HttpGet("{articleid}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.GetByIdAsync(articleId));

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto createArticleDto) =>
            CreateResponse(await _articleService.CreateAsync(createArticleDto));

        [MapToApiVersion(1)]
        [HttpPut("{articleid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute(Name = "articleid")] Guid articleId, [FromBody] UpdateArticleDto updateArticleDto) =>
            CreateResponse(await _articleService.UpdateAsync(articleId, updateArticleDto));

        [MapToApiVersion(1)]
        [HttpDelete("{articleid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.RemoveAsync(articleId));
    }
}
