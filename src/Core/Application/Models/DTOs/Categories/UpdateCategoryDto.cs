namespace Application.Models.DTOs.Categories
{
    public record UpdateCategoryDto
    {
        public Guid CategoryId { get; init; }
        public Guid? ParentId { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
    }
}
