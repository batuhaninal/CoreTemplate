using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Users;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Users;

namespace Application.Abstractions.Services.Users
{
    public interface IUserService
    {
        Task<IPaginatedDataResult<UserItemDto>> GetAllAsync(UserRequestParameter parameter, BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<UserItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);
    }
}
