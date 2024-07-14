using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Writers;
using Application.Models.Tokens;

namespace Application.Abstractions.Services.Auths
{
    public interface IAuthService
    {
        Task<JwtToken> LoginAsync(LoginDto loginDto);
        Task<IBaseResult> RegisterUserAsync(RegisterDto registerDto);
        Task<IBaseResult> RegisterWriterAsync(RegisterWriterDto registerWriterDto);
    }
}
