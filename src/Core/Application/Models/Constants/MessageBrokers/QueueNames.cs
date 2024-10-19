namespace Application.Models.Constants.MessageBrokers
{
    public static class QueueNames
    {
        public const string Default = "default-queue";
        public const string CacheRemove = "cache-remove-queue";
        public const string ArticleFavorite = "article-favorite-queue";

        public const string CreateArticleElastic = "create-article-elastic-queue";
        public const string UpdateArticleElastic = "update-article-elastic-queue";
        public const string RemoveArticleElastic = "remove-article-elastic-queue";

        public const string CreateCategoryElastic = "create-category-elastic-queue";
        public const string UpdateCategoryElastic = "update-category-elastic-queue";
        public const string RemoveCategoryElastic = "remove-category-elastic-queue";

        public const string CreateWriterElastic = "create-writer-elastic-queue";
        public const string UpdateWriterElastic = "update-writer-elastic-queue";
        public const string RemoveWriterElastic = "remove-writer-elastic-queue";

        public const string CreateUserElastic = "create-user-elastic-queue";
        public const string UpdateUserElastic = "update-user-elastic-queue";
        public const string RemoveUserElastic = "remove-user-elastic-queue";
    }

    public static class ExchangeNames
    {
        public const string Default = "default-exchange";
        public const string Cache = "cache-exchange";
        public const string Article = "article-exchange";
        public const string Elastic = "elastic-exchange";
    }
}
