namespace Application.Models.DTOs.Categories
{
    public record ChildrenCategoryItemDto
    {
        public Guid CategoryId { get; init; }
        public string Title { get; init; } = null!;
        public List<ChildrenCategoryItemDto>? Childrens { get; set; }
    }
}
