using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Writers;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Elastics;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.MessageBrokers.Events;
using Application.Models.MessageBrokers.Events.Writers;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Writers;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Writers.Extensions;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Writers
{
    public class WriterService : BaseService, IWriterService
    {
        private readonly WriterBusinessRules _writerBusinessRules;
        public WriterService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService publisher) : base(unitOfWork, mapper, cache, publisher)
        {
            _writerBusinessRules = new WriterBusinessRules(unitOfWork.WriterReadRepository);
        }

        public async Task<IBaseResult> CreateAsync(CreateWriterDto createWriterDto)
        {
            await _writerBusinessRules.CheckNickAvailable(createWriterDto.Nick);
            await _writerBusinessRules.CheckUserIdAvailable(createWriterDto.UserId);

            Writer toCreateEntity = Mapper.Map<Writer>(createWriterDto);
            var createdWriter = await UnitOfWork.WriterWriteRepository.CreateAsync(toCreateEntity);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.CreateWriterElastic, ExchangeNames.Elastic, new WriterCreatedEvent()
            {
                IndexName = ElasticIndexes.WriterIndex,
                Model = JsonSerializer.Serialize(createdWriter)
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(BasePaginationRequestParameter pagination)
        {
            if(pagination.PageIndex <= 5 && (pagination.PageSize == 20 || pagination.PageSize == 50 || pagination.PageSize == 100))
            {
                string? cache = await Cache.GetAsync(CachePrefix.Writer.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize));
                if (!string.IsNullOrEmpty(cache))
                    return JsonSerializer.Deserialize<PaginatedListDto<WriterItemDto>>(cache)!;
            }

            PaginatedListDto<WriterItemDto> data = await UnitOfWork.WriterReadRepository.Table
                .Include(x=> x.User)
                .Select(x=> Mapper.Map<WriterItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            if (pagination.PageIndex <= 5 && (pagination.PageSize == 20 || pagination.PageSize == 50 || pagination.PageSize == 100) && data != null && data.TotalCount > 0)
                await Cache.AddAsync(CachePrefix.Writer.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize), data);

            return data!;
        }

        public async Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(WriterRequestParameter parameter, BasePaginationRequestParameter pagination)
        {
            PaginatedListDto<WriterItemDto> data = await UnitOfWork.WriterReadRepository.Table
                .Include(x=> x.User)
                .Filter(parameter)
                .Select(x => Mapper.Map<WriterItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            return data!;
        }

        public async Task<IDataResult<WriterInfoDto>> GetByIdAsync(string writerId)
        {
            await _writerBusinessRules.CheckWriterExistById(writerId);

            WriterInfoDto writer = (await UnitOfWork.WriterReadRepository.Table
                .Include(x=> x.User)
                .Where(x => x.Id == Guid.Parse(writerId))
                .Select(x => Mapper.Map<WriterInfoDto>(x))
                .FirstOrDefaultAsync())!;

            return new SuccessDataResultDto<WriterInfoDto>(writer);
        }

        public async Task<IBaseResult> RemoveAsync(string writerId)
        {
            await _writerBusinessRules.CheckWriterExistById(writerId);

            await UnitOfWork.WriterWriteRepository.RemoveAsync(writerId);

            Publisher.Publish(QueueNames.RemoveWriterElastic, ExchangeNames.Elastic, new WriterRemovedEvent()
            {
                IndexName = ElasticIndexes.WriterIndex,
                WriterId = writerId
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.CacheRemove, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Writer.Prefix,
                CachePrefix.Articles.Prefix,
            }));
        }
    }
}
