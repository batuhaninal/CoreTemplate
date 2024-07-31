namespace Application.Models.Constants.MessageBrokers
{
    public static class QueueNames
    {
        public const string Default = "default-queue";
        public const string CacheRemove = "cache-remove-queue";
    }

    public static class ExchangeNames
    {
        public const string Default = "default-exchange";
        public const string Cache = "cache-exchange";
    }
}
