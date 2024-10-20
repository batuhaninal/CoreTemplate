namespace Application.Models.MessageBrokers.Events.Writers
{
    public record WriterFavoritedEvent
    {
        public Guid UserId { get; init; }
        public Guid WriterId { get; init; }
    }
}
