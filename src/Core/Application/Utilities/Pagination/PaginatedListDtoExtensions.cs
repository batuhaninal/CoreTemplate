using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;

namespace Application.Utilities.Pagination
{
    public static class PaginatedListDtoExtensions
    {
        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int statusCode, bool isSuccess, string message) =>
            await PaginatedListDto<T>.CreateAsync(source, pageIndex, pageSize, statusCode, isSuccess, message);

        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int statusCode) =>
            await PaginatedListDto<T>.CreateAsync(source, pageIndex, pageSize, statusCode, true);

        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize) =>
            await PaginatedListDto<T>.CreateAsync(source, pageIndex, pageSize, 200, true);

        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, string message) =>
            await PaginatedListDto<T>.CreateAsync(source, pageIndex, pageSize, 200, true, message);

        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, BasePaginationRequestParameter pagination) =>
            await PaginatedListDto<T>.CreateAsync(source, pagination, 200, true);

        public static async Task<PaginatedListDto<T>> ToPaginatedListDtoAsync<T>(this IQueryable<T> source, BasePaginationRequestParameter pagination, string message) =>
            await PaginatedListDto<T>.CreateAsync(source, pagination, 200, true, message);
    }
}
