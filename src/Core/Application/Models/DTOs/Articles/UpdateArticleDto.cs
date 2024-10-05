namespace Application.Models.DTOs.Articles
{
    public record UpdateArticleDto
    {
        public Guid ArticleId { get; init; }
        public string? Title { get; init; }
        public string? Content { get; init; } = null!;
        public Guid CategoryId { get; init; }
    }
}
