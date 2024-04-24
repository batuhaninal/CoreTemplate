namespace Application.Models.DTOs.Categories
{
    public record UpdateCategoryDto
    {
        public string CategoryId { get; init; } = null!;
        public string Title { get; init; } = null!;
    }
}
