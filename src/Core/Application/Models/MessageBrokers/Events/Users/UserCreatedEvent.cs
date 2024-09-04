namespace Application.Models.MessageBrokers.Events.Users
{
    public record UserCreatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
