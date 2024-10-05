using Application.Abstractions.Services.Users;
using Application.Models.Constants.CachePrefixes;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Users;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion(1)]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string condition, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.SearchAsync(condition, pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        [OutputCache(PolicyName = "Pagination1m", Tags = [OutputCacheTag.UserTag])]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.GetAllAsync(pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] UserRequestParameter parameter) =>
            CreateResponse(await _userService.GetAllAsync(parameter));
    }
}
