using API.Attributes;
using Application.Abstractions.Services.Auths;
using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Writers;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion(1)]
    public class AuthsController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [SecureOperation]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto registerDto) =>
            CreateResponse(await _authService.RegisterUserAsync(registerDto));

        [MapToApiVersion(1)]
        [HttpPost]
        [SecureOperation]
        public async Task<IActionResult> WriterSignUp([FromBody] RegisterWriterDto registerWriterDto) =>
            CreateResponse(await _authService.RegisterWriterAsync(registerWriterDto));

        [MapToApiVersion(1)]
        [HttpPost]
        [SecureOperation]
        public async Task<IActionResult> SignIn([FromBody] LoginDto loginDto) =>
            Ok(await _authService.LoginAsync(loginDto));
    }
}
