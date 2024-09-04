namespace Application.Models.MessageBrokers.Events.Users
{
    public record UserRemovedEvent
    {
        public string IndexName { get; init; } = null!;
        public string UserId { get; init; } = null!;
    }
}
