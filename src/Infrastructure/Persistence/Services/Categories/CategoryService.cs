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
using Application.Utilities.Helpers;
using Application.Utilities.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

            Category createdCategory = Mapper.Map<Category>(createCategoryDto)!;

            await InsertOperationAsync(createdCategory);

            // Eski cache sistemi
            //await Cache.DeleteAllWithPrefixAsync(CachePrefix.Categories.All);

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            string cacheKey = CachePrefix.Categories.CreatePaginationPrefix("GetAllAsync", pageIndex, pageSize);
            bool willCache = CacheHelpers.WillCache(pageIndex, pageSize);

            if (willCache)
            {
                string? cacheData = await Cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cacheData))
                    return JsonSerializer.Deserialize<PaginatedListDto<CategoryItemDto>>(cacheData)!;
            }

            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
                .Include(x => x.Parent)
                .Include(x => x.Childrens)
                .AsSplitQuery()
                .OrderQuery("created")
                .ProjectTo<CategoryItemDto>(Mapper.ConfigurationProvider)
                .ToPaginatedListDtoAsync(pageIndex, pageSize, 200);

            if (willCache && data.ItemsCount > 0)
                await Cache.AddAsync(cacheKey, data);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(BasePaginationRequestParameter pagination) =>
            await GetAllAsync(pagination.PageIndex, pagination.PageSize);

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(CategoryRequestParameter parameter)
        {
            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
                .AsNoTracking()
                .FilterAllConditions(parameter)
                .Select(x => Mapper.Map<CategoryItemDto>(x))
                .ToPaginatedListDtoAsync(parameter);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryToolDto>> GetAllBaseCategoriesAsync(int pageIndex = 1, int pageSize = 20)
        {
            string cacheKey = CachePrefix.Categories.CreatePaginationPrefix("GetAllBaseCategoriesAsync", pageIndex, pageSize);
            bool willCache = CacheHelpers.WillCache(pageIndex, pageSize);

            if (willCache)
            {
                string? cache = await Cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cache))
                    return JsonSerializer.Deserialize<PaginatedListDto<CategoryToolDto>>(cache)!;
            }

            var data = await UnitOfWork.CategoryReadRepository.Table
                .AsNoTracking()
                .Where(x => x.ParentId == null)
                .ProjectTo<CategoryToolDto>(Mapper.ConfigurationProvider)
                .ToPaginatedListDtoAsync(pageIndex, pageSize);

            if (willCache && data.ItemsCount > 0)
                await Cache.AddAsync(cacheKey, data);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryToolDto>> GetAllToolsAsync(int pageIndex = 1, int pageSize = 20)
        {
            string cacheKey = CachePrefix.Categories.CreatePaginationPrefix("GetAllToolsAsync", pageIndex, pageSize);
            bool willCache = CacheHelpers.WillCache(pageIndex, pageSize);

            if (willCache)
            {
                string? cache = await Cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cache))
                    return JsonSerializer.Deserialize<PaginatedListDto<CategoryToolDto>>(cache)!;
            }

            var data = await UnitOfWork.CategoryReadRepository.Table
                .AsNoTracking()
                .ProjectTo<CategoryToolDto>(Mapper.ConfigurationProvider)
                .ToPaginatedListDtoAsync(pageIndex, pageSize);

            if (willCache && data.ItemsCount > 0)
                await Cache.AddAsync(cacheKey, data);

            return data;
        }

        public async Task<IDataResult<CategoryItemDto>> GetByIdAsync(Guid categoryId)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            Category? category = await UnitOfWork.CategoryReadRepository.Table
                .AsNoTracking()
                .Where(x => x.Id == categoryId)
                .Include(x => x.Parent)
                .Include(x => x.Childrens)
                //.Include(x => x.Childrens.Where(c => c.IsActive))
                .FirstOrDefaultAsync();

            await UnitOfWork.CategoryReadRepository.GetChildrenRecursiveAsync(category!);
            //await UnitOfWork.CategoryReadRepository.GetChildrenRecursiveAsync(category!, true);

            return new SuccessDataResultDto<CategoryItemDto>(Mapper.Map<CategoryItemDto>(category)!, "Urun bulundu");
        }

        public async Task<IBaseResult> RemoveAsync(Guid categoryId)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            await UnitOfWork.CategoryWriteRepository.RemoveAsync(categoryId);

            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.RemoveCategoryElastic, ExchangeNames.Elastic, new CategoryRemovedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                CategoryId = categoryId.ToString()
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<SearchCategoryDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination)
        {
            var data = await _elkCategoryRepository.FuzzySearchWithPaginationAsync(condition, pagination);

            return Mapper.Map<PaginatedListDto<SearchCategoryDto>>(data);
        }

        public async Task<IBaseResult> UpdateAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            if (!categoryId.Equals(updateCategoryDto.CategoryId))
                throw new Exception("Category Id degerleri eslesmemektedir!");

            await _businessRules.CheckCategoryExist(updateCategoryDto.CategoryId);

            Category? oldCategory = await UnitOfWork.CategoryReadRepository.GetByIdAsync(updateCategoryDto.CategoryId, true);

            Mapper.Map(updateCategoryDto, oldCategory);

            await UpdateOperationAsync(oldCategory!);

            return new SuccessResultDto(204);
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.CacheRemove, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Categories.Prefix,
                CachePrefix.Articles.Prefix,
            }, new string[] { OutputCacheTag.CategoryTag, OutputCacheTag.ArticleTag }));
        }

        public async Task<IBaseResult> ChangeStatusAsync(Guid categoryId, bool isActive)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            var category = await UnitOfWork.CategoryReadRepository.GetByIdAsync(categoryId, false);
            category!.IsActive = isActive;

            await UpdateOperationAsync(category);

            return new SuccessResultDto(204);
        }

        public async Task<IBaseResult> ChangeStatusAsync(Guid categoryId)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            Category? category = await UnitOfWork.CategoryReadRepository.GetByIdAsync(categoryId, false);
            category!.IsActive = !category.IsActive;

            await UpdateOperationAsync(category);

            return new SuccessResultDto(204);
        }

        private async Task InsertOperationAsync(Category category)
        {
            await UnitOfWork.CategoryWriteRepository.CreateAsync(category);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.CreateCategoryElastic, ExchangeNames.Elastic, new CategoryCreatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(category)
            });

            RemoveCachePrefixes();
        }

        private async Task UpdateOperationAsync(Category category)
        {
            UnitOfWork.CategoryWriteRepository.Update(category);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, new CategoryUpdatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(category),
                CategoryId = category.Id.ToString(),
            });

            RemoveCachePrefixes();
        }
    }
}
