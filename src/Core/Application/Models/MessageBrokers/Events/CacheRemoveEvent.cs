namespace Application.Models.MessageBrokers.Events
{
    public record CacheRemoveEvent
    {
        public CacheRemoveEvent(string cachePrefix)
        {
            CachePrefix = cachePrefix;
        }
        public string CachePrefix { get; init; } = null!;
    }
}
