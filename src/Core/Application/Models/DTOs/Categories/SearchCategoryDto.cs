namespace Application.Models.DTOs.Categories
{
    public record SearchCategoryDto
    {
        public Guid CategoryId { get; init; }
        public string Title { get; init; } = null!;
    }
}
