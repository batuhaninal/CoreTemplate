using Application.Abstractions.Services.Users;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.GetAllAsync(pagination));

        [HttpGet]
        public async Task<IActionResult> GetAllFiltered([FromQuery] UserRequestParameter parameter, [FromQuery] PaginationRequestParameter pagination) =>
            CreateResponse(await _userService.GetAllAsync(parameter, pagination));
    }
}
