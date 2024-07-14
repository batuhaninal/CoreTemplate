namespace Application.Models.DTOs.Users
{
    public record UserInfoDto
    {
        public string UserId { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}
