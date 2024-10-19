namespace Application.Models.DTOs.Articles
{
    public record ArticleFavoriteItemDto
    {
        public Guid ArticleFavoriteId { get; init; }
        public Guid ArticleId { get; init; }
        public Guid UserId { get; init; }
    }
}
