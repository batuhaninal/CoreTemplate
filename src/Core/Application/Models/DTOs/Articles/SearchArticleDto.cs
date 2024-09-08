namespace Application.Models.DTOs.Articles
{
    public record SearchArticleDto
    {
        public string ArticleId { get; init; } = null!;
        public string Title { get; init; } = null!;
    }
}
