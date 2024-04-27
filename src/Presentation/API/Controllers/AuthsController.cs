using Application.Abstractions.Services.Auths;
using Application.Models.DTOs.Auths;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto registerDto) =>
            CreateResponse(await _authService.RegisterUserAsync(registerDto));

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginDto loginDto) =>
            Ok(await _authService.LoginAsync(loginDto));
    }
}
