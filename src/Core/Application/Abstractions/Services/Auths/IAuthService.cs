using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Auths;
using Application.Models.Tokens;

namespace Application.Abstractions.Services.Auths
{
    public interface IAuthService
    {
        Task<JwtToken> LoginAsync(LoginDto loginDto);
        Task<IBaseResult> RegisterUserAsync(RegisterDto registerDto);
    }
}
