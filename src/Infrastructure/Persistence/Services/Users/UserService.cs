using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Users;
using Application.Models.Constants.CachePrefixes;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Users;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Users;
using Application.Utilities.Pagination;
using AutoMapper;
using Persistence.Repositories.Users.Extensions;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService publisher) : base(unitOfWork, mapper, cache, publisher)
        {
        }

        public async Task<IPaginatedDataResult<UserItemDto>> GetAllAsync(UserRequestParameter parameter, BasePaginationRequestParameter pagination)
        {
            var users = await UnitOfWork.UserReadRepository.Table
                .Filter(parameter)
                .Select(x => Mapper.Map<UserItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            return users;
        }

        public async Task<IPaginatedDataResult<UserItemDto>> GetAllAsync(BasePaginationRequestParameter pagination)
        {
            if (pagination.PageIndex <= 5 && (pagination.PageSize == 20 || pagination.PageSize == 50 || pagination.PageSize == 100))
            {
                string? cache = await Cache.GetAsync(CachePrefix.User.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize));
                if (!string.IsNullOrEmpty(cache))
                    return JsonSerializer.Deserialize<PaginatedListDto<UserItemDto>>(cache)!;
            }

            var users = await UnitOfWork.UserReadRepository.Table
                .Select(x => Mapper.Map<UserItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            if (pagination.PageIndex <= 5 && (pagination.PageSize == 20 || pagination.PageSize == 50 || pagination.PageSize == 100) && users.ItemsCount > 0)
                await Cache.AddAsync(CachePrefix.User.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize), users);

            return users;
        }
    }
}
