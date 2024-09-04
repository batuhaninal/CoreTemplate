namespace Application.Models.MessageBrokers.Events.Writers
{
    public record WriterRemovedEvent
    {
        public string IndexName { get; init; } = null!;
        public string WriterId { get; init; } = null!;
    }
}
