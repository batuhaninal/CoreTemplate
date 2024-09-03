namespace Application.Models.MessageBrokers.Events.Articles
{
    public record ArticleCreatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
