namespace Application.Models.DTOs.Categories
{
    public record CreateCategoryDto
    {
        public Guid? ParentId { get; init; }
        public string Title { get; init; } = null!;
    }
}
