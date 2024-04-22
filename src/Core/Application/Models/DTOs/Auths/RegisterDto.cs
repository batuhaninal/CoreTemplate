namespace Application.Models.DTOs.Auths
{
    public record RegisterDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
        public string RepeatPassword { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
