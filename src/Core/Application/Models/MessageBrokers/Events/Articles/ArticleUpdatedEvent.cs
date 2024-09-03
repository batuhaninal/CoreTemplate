namespace Application.Models.MessageBrokers.Events.Articles
{
    public record ArticleUpdatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string ArticleId { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
