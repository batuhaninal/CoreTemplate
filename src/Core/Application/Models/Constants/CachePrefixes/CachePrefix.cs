namespace Application.Models.Constants.CachePrefixes
{
    public static class CachePrefix
    {
        public static class Categories
        {
            public const string Prefix = "categories";
            public const string All = $"{Prefix}.all";
            public static string CreatePrefix(string operationName) => string.IsNullOrEmpty(operationName) ? $"{Prefix}.noOperationName" : $"{Prefix}.{operationName}";
            public static string CreatePaginationPrefix(string operationName, int pageIndex, int pageSize) => $"{Prefix}.{operationName}.pageIndex={pageIndex}.pageSize={pageSize}";
            public static string GetAllWithPagination(int pageIndex, int pageSize) => $"{All}.pageIndex={pageIndex}.pageSize={pageSize}";
            public static string GetByParameter(string parameterName, string parameterValue) => $"{Prefix}.getbyid.{parameterName}={parameterValue}";
        }

        public static class Products
        {
            public const string Prefix = "products";
            public const string All = $"{Prefix}.all";
            public static string CreatePrefix(string operationName) => string.IsNullOrEmpty(operationName) ? $"{Prefix}.noOperationName" : $"{Prefix}.{operationName}";
            public static string CreatePaginationPrefix(string operationName, int pageIndex, int pageSize) => $"{Prefix}.{operationName}.pageIndex={pageIndex}.pageSize={pageSize}";
            public static string GetAllWithPagination(int pageIndex, int pageSize) => $"{All}.pageIndex={pageIndex}.pageSize={pageSize}";
            public static string GetByParameter(string parameterName, string parameterValue) => $"{Prefix}.getbyid.{parameterName}={parameterValue}";
        }
    }
}
