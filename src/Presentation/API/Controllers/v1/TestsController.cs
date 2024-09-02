using Application.Abstractions.Services.Articles;
using Application.Abstractions.Services.Categories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion(1)]
    public class TestsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public TestsController(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public async Task<IActionResult> Test1([FromRoute] string articleId)
        {
            return Ok(await _articleService.Test1(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public async Task<IActionResult> Test2([FromRoute] string articleId)
        {
            return Ok(await _articleService.Test2(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public async Task<IActionResult> Test3([FromRoute] string articleId)
        {
            return Ok(await _articleService.Test3(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public async Task<IActionResult> Test4([FromRoute] string articleId)
        {
            return Ok(await _articleService.Test4(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public IActionResult Test5([FromRoute] string articleId)
        {
            return Ok(_articleService.Test5(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public IActionResult Test6([FromRoute] string articleId)
        {
            return Ok(_articleService.Test6(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public IActionResult Test7([FromRoute] string articleId)
        {
            return Ok(_articleService.Test7(articleId));
        }

        [MapToApiVersion(1)]
        [HttpGet("{articleId}")]
        public IActionResult Test8([FromRoute] string articleId)
        {
            return Ok(_articleService.Test8(articleId));
        }

    }
}
