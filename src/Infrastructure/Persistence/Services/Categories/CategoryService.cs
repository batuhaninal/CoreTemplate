using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Categories.Elasticsearch;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.Elastics;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Commons.Results;
using Application.Models.MessageBrokers.Events;
using Application.Models.MessageBrokers.Events.Categories;
using Application.Models.RequestParameters.Categories;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.OutputCaching;
using Persistence.Repositories.Categories.Extensions;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly CategoryBusinessRules _businessRules;
        private readonly IELKCategoryRepository _elkCategoryRepository;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService rabbitMQPublisherService, IELKCategoryRepository elkCategoryRepository) : base(unitOfWork, mapper, cache, rabbitMQPublisherService)
        {
            _businessRules = new CategoryBusinessRules(unitOfWork.CategoryReadRepository);
            _elkCategoryRepository = elkCategoryRepository;
        }

        public async Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            await _businessRules.CheckTitleDuplicate(createCategoryDto.Title);

            var createdCategory = await UnitOfWork.CategoryWriteRepository.CreateAsync(Mapper.Map<Category>(createCategoryDto));

            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.CreateCategoryElastic, ExchangeNames.Elastic, new CategoryCreatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(createdCategory)   
            });

            // Eski cache sistemi
            //await Cache.DeleteAllWithPrefixAsync(CachePrefix.Categories.All);

            RemoveCachePrefixes();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            string cacheKey = CachePrefix.Categories.CreatePaginationPrefix("GetAllAsync", pageIndex, pageSize);
            if(pageIndex > 0 && pageIndex <= 5 && (pageSize == 5 || pageSize == 10 || pageSize == 20 || pageSize == 25 || pageSize == 50))
            {
                string? cacheData = await Cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cacheData))
                    return JsonSerializer.Deserialize<PaginatedListDto<CategoryItemDto>>(cacheData)!;
            }

            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
                .Select(x => Mapper.Map<CategoryItemDto>(x))
                .ToPaginatedListDtoAsync(pageIndex, pageSize, 200);

            if (pageIndex > 0 && pageIndex <= 5 && (pageSize == 5 || pageSize == 10 || pageSize == 20 || pageSize == 25 || pageSize == 50) && data.ItemsCount > 0)
                await Cache.AddAsync(cacheKey, data);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(BasePaginationRequestParameter pagination) => 
            await GetAllAsync(pagination.PageIndex, pagination.PageSize);

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(CategoryRequestParameter parameter, BasePaginationRequestParameter pagination)
        {
            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
                .FilterAllConditions(parameter)
                .Select(x => Mapper.Map<CategoryItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            return data;
        }

        public async Task<IDataResult<CategoryItemDto>> GetByIdAsync(string id)
        {
            await _businessRules.CheckCategoryExist(id);

            var category = await UnitOfWork.CategoryReadRepository.GetByIdAsync(id);

            return new SuccessDataResultDto<CategoryItemDto>(Mapper.Map<CategoryItemDto>(category)!, "Urun bulundu");
        }

        public async Task<IBaseResult> RemoveAsync(string id)
        {
            await _businessRules.CheckCategoryExist(id);

            await UnitOfWork.CategoryWriteRepository.RemoveAsync(id);

            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.RemoveCategoryElastic, ExchangeNames.Elastic, new CategoryRemovedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                CategoryId = id
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<SearchCategoryDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination)
        {
            var data = await _elkCategoryRepository.FuzzySearchWithPaginationAsync(condition, pagination);

            return Mapper.Map<PaginatedListDto<SearchCategoryDto>>(data);   
        }

        public async Task<IBaseResult> UpdateAsync(string categoryId, UpdateCategoryDto updateCategoryDto)
        {
            if (!categoryId.Equals(updateCategoryDto.CategoryId))
                throw new Exception("Category Id degerleri eslesmemektedir!");

            await _businessRules.CheckCategoryExist(updateCategoryDto.CategoryId);

            var oldCategory = await UnitOfWork.CategoryReadRepository.GetByIdAsync(updateCategoryDto.CategoryId, true);

            Mapper.Map(updateCategoryDto, oldCategory);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, new CategoryUpdatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(oldCategory),
                CategoryId = categoryId
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.CacheRemove, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Categories.Prefix,
                CachePrefix.Articles.Prefix,
            }, new string[] { OutputCacheTag.CategoryTag }));
        }
    }
}
