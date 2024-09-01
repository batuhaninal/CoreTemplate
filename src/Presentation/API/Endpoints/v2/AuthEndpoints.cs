using Application.Abstractions.Services.Auths;
using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Writers;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.v2
{
    public static class AuthEndpoints
    {
        public static async Task<IResult> SignUp([FromBody] RegisterDto registerDto, IAuthService authService)
        {
            var result = await authService.RegisterUserAsync(registerDto);

            return Results.Ok(result);
        }

        public static async Task<IResult> WriterSignUp([FromBody] RegisterWriterDto registerWriterDto, IAuthService authService) 
        {
            var result = await authService.RegisterWriterAsync(registerWriterDto);

            return Results.Ok(result);
        }
        public static async Task<IResult> SignIn([FromBody] LoginDto loginDto, IAuthService authService)
        {
            var result = await authService.LoginAsync(loginDto);

            return Results.Ok(result);
        }
    }
}
