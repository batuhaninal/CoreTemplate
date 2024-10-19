using Application.Abstractions.Services.Articles;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Options;
using Application.Models.Constants.Roles;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Gets all articles with pagination",
                  Description = "Retrieves a paginated list of all articles. Caching is applied with a policy of 1 minute.")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter parameter) =>
            CreateResponse(await _articleService.GetAllAsync(parameter.PageIndex, parameter.PageSize));

        [MapToApiVersion(1)]
        [HttpGet]
        [SwaggerOperation(Summary = "Searches for articles",
                  Description = "Searches articles based on a condition and returns the paginated results.")]
        public async Task<IActionResult> Search([FromQuery] string condition, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _articleService.SearchAsync(condition, pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.ArticleTag])]
        [SwaggerOperation(Summary = "Gets all filtered articles",
                  Description = "Retrieves a paginated list of articles filtered based on the provided parameters. Caching is applied with a policy of 1 minute.")]
        public async Task<IActionResult> GetAllFiltered([FromQuery] ArticleRequestParameter parameter) =>
            CreateResponse(await _articleService.GetAllAsync(parameter));

        [MapToApiVersion(1)]
        [HttpGet("{articleid}")]
        [SwaggerOperation(Summary = "Gets an article by ID",
                  Description = "Retrieves a specific article based on the provided article ID.")]
        public async Task<IActionResult> GetById([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.GetByIdAsync(articleId));

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = AppRoles.Writer)]
        [SwaggerOperation(Summary = "Create an article.",
                  Description = "Requires authorization with Writer Role. Create an article.")]
        public async Task<IActionResult> Create([FromBody] CreateArticleDto createArticleDto) =>
            CreateResponse(await _articleService.CreateAsync(createArticleDto));

        [MapToApiVersion(1)]
        [HttpPut("{articleid}")]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Writer}")]
        [SwaggerOperation(Summary = "Update an article with given articleid",
                  Description = "Requires authorization with Admin Role or current article's Writer. Update status the article with the provided articleId.")]
        public async Task<IActionResult> Update([FromRoute(Name = "articleid")] Guid articleId, [FromBody] UpdateArticleDto updateArticleDto) =>
            CreateResponse(await _articleService.UpdateAsync(articleId, updateArticleDto));

        [MapToApiVersion(1)]
        [HttpPut("{articleid}")]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Writer}")]
        [SwaggerOperation(Summary = "Changing status an article with given articleid",
                  Description = "Requires authorization with Admin Role or current article's Writer. Changing status the article with the provided articleId.")]
        public async Task<IActionResult> ChangeStatus([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.ChangeStatusAsync(articleId));

        [MapToApiVersion(1)]
        [HttpDelete("{articleid}")]
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Writer}")]
        [SwaggerOperation(Summary = "Remove an article.",
                  Description = "Requires authorization with Admin Role. Remove the article with the provided articleId.")]
        public async Task<IActionResult> Remove([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.RemoveAsync(articleId));

        [MapToApiVersion(1)]
        [HttpPost("{articleid}")]
        [Authorize]
        [SwaggerOperation(Summary = "Adds an article to the user's favorites",
                  Description = "Requires authorization. Adds the article with the provided articleId to the current user's favorites.")]
        public async Task<IActionResult> AddToFav([FromRoute(Name = "articleid")] Guid articleId) =>
            CreateResponse(await _articleService.AddToFavAsync(articleId));
    }
}
