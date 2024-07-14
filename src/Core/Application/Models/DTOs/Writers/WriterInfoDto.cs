using Application.Models.Enums;

namespace Application.Models.DTOs.Writers
{
    public class WriterInfoDto
    {
        public string WriterId { get; init; } = null!;
        public string Nick { get; init; } = null!;
        public WriterLevel Level { get; init; }
    }
}
