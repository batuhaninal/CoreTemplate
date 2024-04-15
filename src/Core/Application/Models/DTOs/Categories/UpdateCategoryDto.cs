namespace Application.Models.DTOs.Categories
{
    public record UpdateCategoryDto
    {
        public Guid CategoryId { get; init; }
        public string? Title { get; init; }
    }
}
