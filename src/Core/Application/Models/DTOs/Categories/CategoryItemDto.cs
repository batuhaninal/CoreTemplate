namespace Application.Models.DTOs.Categories
{
    public record CategoryItemDto
    {
        public Guid CategoryId { get; init; }
        public string Title { get; set; } = null!;
        public DateTime CreatedDate { get; init; }
        public ParentCategoryItemDto? Parent { get; init; }
        public List<ChildrenCategoryItemDto>? Childrens { get; init; }
    }
}
