using Application.Abstractions.Services.Writers;
using Application.Models.Constants.CachePrefixes;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Writers;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion(1)]
    public class WritersController : BaseController
    {
        private readonly IWriterService _writerService;

        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string condition, [FromQuery] BasePaginationRequestParameter pagination) =>
             CreateResponse(await _writerService.SearchAsync(condition, pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.WriterTag])]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter paginationRequestParameter) =>
             CreateResponse(await _writerService.GetAllAsync(paginationRequestParameter));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.WriterTag])]
        public async Task<IActionResult> GetAllFiltered([FromQuery] WriterRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
             CreateResponse(await _writerService.GetAllAsync(parameter, pagination));

        [MapToApiVersion(1)]
        [HttpGet("{writerid}")]
        public async Task<IActionResult> GetById([FromRoute(Name = "writerid")] Guid writerId) =>
             CreateResponse(await _writerService.GetByIdAsync(writerId));
    }
}
