namespace Application.Models.MessageBrokers.Events.Writers
{
    public record WriterCreatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
