namespace Application.Models.DTOs.Auths
{
    public record LoginDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
