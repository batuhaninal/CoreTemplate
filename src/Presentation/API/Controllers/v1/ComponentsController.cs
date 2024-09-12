using Application.Abstractions.Services.Articles;
using Application.Abstractions.Services.Categories;
using Application.Abstractions.Services.Writers;
using Application.Models.ViewModels;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion(1)]
    public class ComponentsController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IWriterService _writerService;

        public ComponentsController(IArticleService articleService, ICategoryService categoryService, IWriterService writerService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _writerService = writerService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> SearchComponent([FromQuery] string condition, [FromQuery] int size = 10)
        {
            var articles = await _articleService.SearchAsync(condition, size);
            var categories = await _categoryService.SearchAsync(condition, size);
            var writers = await _writerService.SearchAsync(condition, size);
            return Ok(new SearchComponentViewModel(writers.Data, categories.Data, articles.Data));
        }
    }
}
