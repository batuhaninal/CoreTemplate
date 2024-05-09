namespace Application.Models.DTOs.Articles
{
    public record ArticleItemDto
    {
        public string ArticleId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string CategoryId { get; init; } = null!;
        public DateTime CreatedDate { get; init; }
    }
}
