namespace Application.Models.DTOs.Writers
{
    public record CreateWriterDto
    {
        public string Nick { get; init; } = null!;
        public Guid UserId { get; init; }
    }
}
