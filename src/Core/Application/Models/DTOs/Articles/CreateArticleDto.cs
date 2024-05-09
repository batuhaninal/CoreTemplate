namespace Application.Models.DTOs.Articles
{
    public record CreateArticleDto
    {
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public string CategoryId { get; init; } = null!;
    }
}
