using Domain.Entities;

namespace Application.Models.DTOs.Users
{
    public record UserItemDto
    {
        public UserItemDto()
        {
            
        }
        public UserItemDto(Guid userId, string email, string fullName)
        {
            UserId = userId.ToString();
            Email = email;
            FullName = fullName;
        }

        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserItemDto Create(User x)
        {
            return new UserItemDto
            {
                UserId = x.Id.ToString(),
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
            };
        }
    }
}
