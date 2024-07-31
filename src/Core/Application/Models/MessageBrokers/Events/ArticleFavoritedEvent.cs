namespace Application.Models.MessageBrokers.Events
{
    public record ArticleFavoritedEvent
    {
        public Guid ArticleId { get; init; }
        public Guid UserId { get; init; }
    }
}
