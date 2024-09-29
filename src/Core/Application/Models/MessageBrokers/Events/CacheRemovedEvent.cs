namespace Application.Models.MessageBrokers.Events
{
    public record CacheRemovedEvent
    {
        public CacheRemovedEvent()
        {
            CachePrefixes = [];
            OutputCacheTags = [];
        }
        public CacheRemovedEvent(IEnumerable<string> cachePrefixes, IEnumerable<string> outputCacheTags)
        {
            CachePrefixes = cachePrefixes.ToArray();
            OutputCacheTags = outputCacheTags.ToArray();
        }
        public string[] CachePrefixes { get; init; } = null!;
        public string[] OutputCacheTags { get; init; } = null!;
    }
}
