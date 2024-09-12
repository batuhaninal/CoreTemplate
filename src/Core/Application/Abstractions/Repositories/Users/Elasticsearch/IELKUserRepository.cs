using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Users;
using Application.Models.RequestParameters.Commons;
using System.Collections.Immutable;

namespace Application.Abstractions.Repositories.Users.Elasticsearch
{
    public interface IELKUserRepository
    {
        Task<IImmutableList<SecuredUserDto>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IImmutableList<SecuredUserDto>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<SecuredUserDto>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10);
        Task<IPaginatedDataResult<SecuredUserDto>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
