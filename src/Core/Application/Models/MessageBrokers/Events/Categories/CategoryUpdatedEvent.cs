namespace Application.Models.MessageBrokers.Events.Categories
{
    public record CategoryUpdatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string CategoryId { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
