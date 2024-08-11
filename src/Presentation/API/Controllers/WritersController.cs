using Application.Abstractions.Services.Writers;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Writers;
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

        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] WriterRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
             CreateResponse(await _writerService.GetAllAsync(parameter, pagination));

        [HttpGet("{writerid}")]
        public async Task<IActionResult> GetAll([FromRoute(Name = "writerid")] string writerId) =>
             CreateResponse(await _writerService.GetByIdAsync(writerId));
    }
}
