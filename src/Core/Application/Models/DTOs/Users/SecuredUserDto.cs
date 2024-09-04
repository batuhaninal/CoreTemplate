using Domain.Entities;

namespace Application.Models.DTOs.Users
{
    public record SecuredUserDto
    {
        public SecuredUserDto()
        {
            
        }
        public SecuredUserDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CreatedDate = user.CreatedDate;
            IsActive = user.IsActive;
            UpdatedDate = user.UpdatedDate;
        }
        public Guid Id { get; init; }
        public DateTime CreatedDate { get; init; }
        virtual public DateTime UpdatedDate { get; init; }
        public bool IsActive { get; init; }
        public string Email { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
