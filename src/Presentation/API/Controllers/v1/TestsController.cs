using Application.Abstractions.Commons.Files;
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
        private readonly IFileService _fileService;

        public TestsController(ICategoryService categoryService, IArticleService articleService, IFileService fileService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _fileService = fileService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await _categoryService.GetAllAsync(pageIndex, pageSize));
        }

        //[MapToApiVersion(1)]
        //[HttpPost]
        //public async Task<IActionResult> UploadFile([FromForm] IFormFile formFile)
        //{
        //    var result = await _fileService.UploadAsync(new Application.Models.DTOs.Commons.Files.CreateFileDto(){
        //        Path = "test",
        //        FormFiles = new FormFileCollection() { formFile }
        //    });
        //    return Ok(result);
        //}
    }
}
