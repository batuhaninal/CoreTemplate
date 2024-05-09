namespace Application.Models.DTOs.Articles
{
    public record UpdateArticleDto
    {
        public string ArticleId { get; init; } = null!;
        public string? Title { get; init; }
        public string? Content { get; init; } = null!;
        public string CategoryId { get; init; } = null!;
    }
}
