using Application.Models.Enums;

namespace Application.Models.DTOs.Writers
{
    public record WriterItemDto
    {
        public string WriterId { get; init; } = null!;
        public string Nick { get; init; } = null!;
        public string Level { get; init; } = null!;
    }
}
