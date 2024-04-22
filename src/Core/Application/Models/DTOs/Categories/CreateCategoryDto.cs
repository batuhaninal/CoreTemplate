namespace Application.Models.DTOs.Categories
{
    public record CreateCategoryDto
    {
        public string Title { get; init; } = null!;
    }
}
