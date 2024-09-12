using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;
using Domain.Entities;
using System.Collections.Immutable;

namespace Application.Abstractions.Repositories.Categories.Elasticsearch
{
    public interface IELKCategoryRepository
    {
        Task<IImmutableList<Category>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IImmutableList<Category>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<Category>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IPaginatedDataResult<Category>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
