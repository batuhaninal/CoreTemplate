namespace Application.Models.DTOs.Categories
{
    public record ParentCategoryItemDto
    {
        public Guid CategoryId { get; init; }
        public string Title { get; init; } = null!;
        public ParentCategoryItemDto? Parent { get; init; }
    }
}
