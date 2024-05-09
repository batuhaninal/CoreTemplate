using Application.Abstractions.Commons.Results;
using Application.Models.Constants.Settings;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Application.Models.DTOs.Commons.Results
{
    public class PaginatedListDto<T> : ResultDto, IPaginatedDataResult<T>
    {
        [JsonConstructor]
        public PaginatedListDto() : base(200, true)
        {
            
        }
        public PaginatedListDto(List<T> data, int count, int pageIndex, int pageSize, int statusCode, bool isSuccess, string message) : 
            base(statusCode, isSuccess, message)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            ItemsCount = data.Count;
            Data = data;
        }

        public PaginatedListDto(List<T> data, int count, int pageIndex, int pageSize, int statusCode, bool isSuccess) :
            base(statusCode, isSuccess)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            ItemsCount = data.Count;
            Data = data;
        }
        public int TotalCount { get; set; }

        public int ItemsCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get => PageIndex > 1; set { } }

        public bool HasNextPage { get => PageIndex < TotalPages; set { } }

        public IEnumerable<T> Data { get; set; }

        public static async Task<PaginatedListDto<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize, int statusCode, bool isSuccess, string message)
        {
            if(pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1 || pageSize > SettingConstant.PaginationSettings.MaxPageSize)
                pageSize = 20;

            int count = await query.CountAsync();
            var data = await query
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedListDto<T>(data, count, pageIndex, pageSize, statusCode, isSuccess, message);
        }

        public static async Task<PaginatedListDto<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize, int statusCode, bool isSuccess)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1 || pageSize > 2500)
                pageSize = 20;

            int count = await query.CountAsync();
            var data = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedListDto<T>(data, count, pageIndex, pageSize, statusCode, isSuccess);
        }
    }
} 
