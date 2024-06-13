namespace Application.Models.MessageBrokers.Events
{
    public record CacheRemovedEvent
    {
        public CacheRemovedEvent()
        {
            
        }
        public CacheRemovedEvent(string[] cachePrefixes)
        {
            CachePrefixes = cachePrefixes;
        }
        public string[] CachePrefixes { get; init; } = null!;
    }
}
