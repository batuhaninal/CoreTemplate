namespace Application.Models.DTOs.Users
{
    public record UserInfoDto
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}
