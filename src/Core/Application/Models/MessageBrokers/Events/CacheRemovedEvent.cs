namespace Application.Models.MessageBrokers.Events
{
    public record CacheRemovedEvent
    {
        public CacheRemovedEvent(string cachePrefix)
        {
            CachePrefix = cachePrefix;
        }
        public string CachePrefix { get; init; } = null!;
    }
}
