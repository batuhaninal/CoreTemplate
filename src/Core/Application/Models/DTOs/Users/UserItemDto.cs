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
            UserId = userId;
            Email = email;
            FullName = fullName;
        }

        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserItemDto Create(User x)
        {
            return new UserItemDto
            {
                UserId = x.Id,
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
            };
        }
    }
}
