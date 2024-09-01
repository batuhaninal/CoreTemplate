using Application.Abstractions.Services.Users;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Users;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.GetAllAsync(pagination));

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] UserRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.GetAllAsync(parameter, pagination));
    }
}
