namespace Application.Models.MessageBrokers.Events.Articles
{
    public record ArticleRemovedEvent
    {
        public string IndexName { get; init; } = null!;
        public string ArticleId { get; init; } = null!;
    }
}
