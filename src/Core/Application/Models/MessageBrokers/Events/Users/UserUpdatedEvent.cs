namespace Application.Models.MessageBrokers.Events.Users
{
    public record UserUpdatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
