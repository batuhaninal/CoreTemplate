namespace Application.Models.MessageBrokers.Events.Categories
{
    public record CategoryRemovedEvent
    {
        public string IndexName { get; init; } = null!;
        public string CategoryId { get; init; } = null!;
    }
}
