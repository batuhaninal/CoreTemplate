using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;
using Domain.Entities;
using System.Collections.Immutable;

namespace Application.Abstractions.Repositories.Writers.Elasticsearch
{
    public interface IELKWriterRepository
    {
        Task<IImmutableList<Writer>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IImmutableList<Writer>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination);
        Task<PaginatedListDto<Writer>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<PaginatedListDto<Writer>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
