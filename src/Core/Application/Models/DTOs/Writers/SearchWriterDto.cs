namespace Application.Models.DTOs.Writers
{
    public record SearchWriterDto
    {
        public string WriterId { get; init; } = null!;
        public string Nick { get; init; } = null!;
    }
}
