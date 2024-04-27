using Application.Models.Tokens;
using Domain.Entities.Identities;

namespace Application.Abstractions.Commons.Tokens
{
    public interface ITokenService
    {
        JwtToken CreateAccessToken(User user, string role, int minutes);
        string CreateRefreshToken();
    }
}
