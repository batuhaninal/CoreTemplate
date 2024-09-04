namespace Application.Models.MessageBrokers.Events.Writers
{
    public record WriterUpdatedEvent
    {
        public string IndexName { get; init; } = null!;
        public string WriterId { get; init; } = null!;
        public string Model { get; init; } = null!;
    }
}
