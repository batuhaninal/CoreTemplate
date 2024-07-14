using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Writers;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.MessageBrokers.Events;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
            Writer toCreateEntity = Mapper.Map<Writer>(createWriterDto);
            await UnitOfWork.WriterWriteRepository.CreateAsync(toCreateEntity);
            await UnitOfWork.SaveChangesAsync();

            RemoveCachePrefixes();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(BasePaginationRequestParameter pagination)
        {
            string? cache = await Cache.GetAsync(CachePrefix.Writer.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize));
            if (!string.IsNullOrEmpty(cache))
                return JsonSerializer.Deserialize<PaginatedListDto<WriterItemDto>>(cache)!;

            PaginatedListDto<WriterItemDto> data = await UnitOfWork.WriterReadRepository.Table
                .Select(x=> Mapper.Map<WriterItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            if (data != null && data.TotalCount > 0)
                await Cache.AddAsync(CachePrefix.Writer.CreatePaginationPrefix("GetAllAsync", pagination.PageIndex, pagination.PageSize), data);

            return data!;
        }

        public async Task<IDataResult<WriterInfoDto>> GetByIdAsync(string writerId)
        {
            await _writerBusinessRules.CheckWriterExistById(writerId);

            WriterInfoDto writer = (await UnitOfWork.WriterReadRepository.Table
                .Where(x => x.Id == Guid.Parse(writerId))
                .Select(x => Mapper.Map<WriterInfoDto>(x))
                .FirstOrDefaultAsync())!;

            return new SuccessDataResultDto<WriterInfoDto>(writer);
        }

        public async Task<IBaseResult> RemoveAsync(string writerId)
        {
            await _writerBusinessRules.CheckWriterExistById(writerId);

            await UnitOfWork.WriterWriteRepository.RemoveAsync(writerId);

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.Cache, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Writer.Prefix,
                CachePrefix.Articles.Prefix,
            }));
        }
    }
}
