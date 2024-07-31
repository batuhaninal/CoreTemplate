namespace Application.Models.MessageBrokers.Events.Articles
{
    public record ArticleFavoritedEvent
    {
        public Guid ArticleId { get; init; }
        public Guid UserId { get; init; }
    }
}
