using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;
using Domain.Entities;
using System.Collections.Immutable;

namespace Application.Abstractions.Repositories.Articles.Elasticsearch
{
    public interface IELKArticleRepository
    {
        Task<IImmutableList<Article>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IImmutableList<Article>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination);
        Task<PaginatedListDto<Article>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<PaginatedListDto<Article>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
