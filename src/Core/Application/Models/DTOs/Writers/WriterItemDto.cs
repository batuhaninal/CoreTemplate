using Application.Models.DTOs.Users;

namespace Application.Models.DTOs.Writers
{
    public record WriterItemDto
    {
        public Guid WriterId { get; init; }
        public string Nick { get; init; } = null!;
        public string Level { get; init; } = null!;
        public UserItemDto User { get; set; } = null!;
    }
}
