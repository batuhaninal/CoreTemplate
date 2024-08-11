namespace Application.Models.DTOs.Users
{
    public record UserItemDto
    {
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
