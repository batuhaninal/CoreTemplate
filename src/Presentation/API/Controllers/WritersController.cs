using Application.Abstractions.Services.Writers;
using Application.Models.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WritersController : BaseController
    {
        private readonly IWriterService _writerService;

        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter paginationRequestParameter) =>
             CreateResponse(await _writerService.GetAllAsync(paginationRequestParameter));

        [HttpGet("{writerId}")]
        public async Task<IActionResult> GetAll([FromRoute(Name = "writerId")] string writerId) =>
             CreateResponse(await _writerService.GetByIdAsync(writerId));
    }
}
