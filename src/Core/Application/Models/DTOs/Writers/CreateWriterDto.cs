namespace Application.Models.DTOs.Writers
{
    public record CreateWriterDto
    {
        public string Nick { get; init; } = null!;
        public string UserId { get; init; } = null!;
    }
}
