namespace Application.Models.DTOs.Users
{
    public record UpdateUserDto
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
