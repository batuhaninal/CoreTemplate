using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }
}
