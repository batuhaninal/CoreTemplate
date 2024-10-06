using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Commons.Tokens;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Repositories.Users.Elasticsearch;
using Application.Abstractions.Services.Users;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Elastics;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Users;
using Application.Models.MessageBrokers.Events;
using Application.Models.MessageBrokers.Events.Users;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Users;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Users.Extensions;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly IELKUserRepository _elkUserRepository;
        private readonly UserBusinessRules _userBusinessRules;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService publisher, IELKUserRepository elkUserRepository, IUserTokenService userTokenService) : base(unitOfWork, mapper, cache, publisher, userTokenService)
        {
            _elkUserRepository = elkUserRepository;
            _userBusinessRules = new UserBusinessRules(UnitOfWork.UserReadRepository);
        }

        public async Task<IBaseResult> ChangeStatusAsync(Guid userId)
        {
            await _userBusinessRules.CheckExist(userId);
            
            User? user = await UnitOfWork.UserReadRepository.GetByIdAsync(userId);
            user!.IsActive = !user.IsActive;

            await UpdateOperationAsync(user);

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<UserItemDto>> GetAllAsync(UserRequestParameter parameter)
        {
            //var users = await UnitOfWork.UserReadRepository.Table
            //    .AsNoTracking()
            //    .Filter(parameter)
            //    .ProjectTo<UserItemDto>(Mapper.ConfigurationProvider)
            //    .ToPaginatedListDtoAsync(pagination);

            var users = await UnitOfWork.UserReadRepository.Table
                .AsNoTracking()
                .FilterAllConditions(parameter)
                .Select(x => Mapper.Map<UserItemDto>(x)!)
                .ToPaginatedListDtoAsync(parameter);

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
                .AsNoTracking()
                .Select(x => Mapper.Map<UserItemDto>(x)!)
                .ToPaginatedListDtoAsync(pagination);

            if (pagination.PageIndex <= 5 && (pagination.PageSize == 20 || pagination.PageSize == 50 || pagination.PageSize == 100) && users.ItemsCount > 0)
                await Cache.AddAsync(CachePrefix.User.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize), users);

            return users;
        }

        public async Task<IPaginatedDataResult<SearchUserDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination)
        {
            var data = await _elkUserRepository.FuzzySearchWithPaginationAsync(condition, pagination);

            return Mapper.Map<PaginatedListDto<SearchUserDto>>(data);
        }

        private async Task InsertOperationAsync(User user)
        {
            await UnitOfWork.UserWriteRepository.CreateAsync(user);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.CreateUserElastic, ExchangeNames.Elastic, new UserCreatedEvent()
            {
                IndexName = ElasticIndexes.UserIndex,
                Model = JsonSerializer.Serialize(new SecuredUserDto(user)),
            });

            RemoveCachePrefixes();
        }

        private async Task UpdateOperationAsync(User user)
        {
            UnitOfWork.UserWriteRepository.Update(user);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.CreateUserElastic, ExchangeNames.Elastic, new UserUpdatedEvent()
            {
                IndexName = ElasticIndexes.UserIndex,
                Model = JsonSerializer.Serialize(new SecuredUserDto(user)),
                UserId = user.Id.ToString()
            });

            RemoveCachePrefixes();
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.CacheRemove, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.User.Prefix,
                CachePrefix.Writer.Prefix,
            }, new string[] { OutputCacheTag.UserTag, OutputCacheTag.WriterTag }));
        }
    }
}
