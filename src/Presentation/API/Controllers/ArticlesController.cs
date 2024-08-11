using Application.Abstractions.Services.Articles;
using Application.Models.Constants.Options;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
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
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter parameter) =>
            CreateResponse(await _articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize));

        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] ArticleRequestParameter parameter,[FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _articleService.GetAllAsync(parameter, pagination));

        [HttpGet("{articleid}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "articleid")] string articleId) => 
            CreateResponse(await _articleService.GetByIdAsync(articleId));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto createArticleDto) =>
            CreateResponse(await _articleService.CreateAsync(createArticleDto));

        [HttpPut("{articleid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute(Name = "articleid")] string articleId, [FromBody] UpdateArticleDto updateArticleDto) =>
            CreateResponse(await _articleService.UpdateAsync(articleId, updateArticleDto));

        [HttpDelete("{articleid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove([FromRoute(Name = "articleid")] string articleId) =>
            CreateResponse(await _articleService.RemoveAsync(articleId));
    }
}
