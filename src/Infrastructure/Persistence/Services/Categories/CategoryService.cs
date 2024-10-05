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
            if (pageIndex > 0 && pageIndex <= 5 && (pageSize == 5 || pageSize == 10 || pageSize == 20 || pageSize == 25 || pageSize == 50))
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

            if (pageIndex > 0 && pageIndex <= 5 && (pageSize == 5 || pageSize == 10 || pageSize == 20 || pageSize == 25 || pageSize == 50) && data.ItemsCount > 0)
                await Cache.AddAsync(cacheKey, data);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(BasePaginationRequestParameter pagination) =>
            await GetAllAsync(pagination.PageIndex, pagination.PageSize);

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(CategoryRequestParameter parameter, BasePaginationRequestParameter pagination)
        {
            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
                .AsNoTracking()
                .FilterAllConditions(parameter)
                .Select(x => Mapper.Map<CategoryItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

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

            await GetChildrenRecursiveAsync(category!);
            //await GetChildrenRecursiveAsync(category!, true);

            return new SuccessDataResultDto<CategoryItemDto>(Mapper.Map<CategoryItemDto>(category)!, "Urun bulundu");
        }

        private async Task GetChildrenRecursiveAsync(Category category)
        {
            if (category.Childrens != null && category.Childrens.Any())
            {
                foreach (var child in category.Childrens)
                {
                    // Alt children'ları veritabanından çekmek için sorgu atıyoruz
                    var loadedChild = await UnitOfWork.CategoryReadRepository.Table
                        .AsNoTracking()
                        .Where(c => c.Id == child.Id)
                        .Include(c => c.Childrens) // Alt children'ları yüklüyoruz
                        .FirstOrDefaultAsync();

                    if (loadedChild != null && loadedChild.Childrens != null && loadedChild.Childrens.Any())
                    {
                        // Child kategorinin childrens listesini dolduruyoruz
                        await GetChildrenRecursiveAsync(loadedChild);

                        // Child'ı orijinal child'a ekliyoruz
                        child.Childrens = loadedChild.Childrens;
                    }
                }
            }
        }

        private async Task GetChildrenRecursiveAsync(Category category, bool isActive)
        {
            // Eğer mevcut kategorinin children'ları varsa devam ediyoruz
            if (category.Childrens != null && category.Childrens.Any())
            {
                // Sadece aktif olan child'lar üzerinde işlem yapıyoruz
                var activeChildren = category.Childrens.Where(c => c.IsActive == isActive).ToList();

                // Hiyerarşik yapı için her bir child üzerinde işlem
                foreach (var child in activeChildren)
                {
                    // Alt children'ları veritabanından çekmek için sorgu atıyoruz
                    var loadedChild = await UnitOfWork.CategoryReadRepository.Table
                        .AsNoTracking()
                        .Where(c => c.Id == child.Id && c.IsActive == isActive)  // Sadece aktif child
                        .Include(c => c.Childrens.Where(x => x.IsActive == isActive))  // Sadece aktif alt children'ları yüklüyoruz
                        .FirstOrDefaultAsync();

                    if (loadedChild != null && loadedChild.Childrens.Any())
                    {
                        // Özyineleme ile alt children'ları getiriyoruz
                        await GetChildrenRecursiveAsync(loadedChild, isActive);

                        // Child'ı orijinal child'a ekliyoruz, sadece aktif children'ları getiriyoruz
                        child.Childrens = loadedChild.Childrens.Where(x => x.IsActive == isActive).ToList();
                    }
                }

                // Son olarak kategorinin children listesini sadece aktif children'larla güncelliyoruz
                category.Childrens = activeChildren;
            }
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

            var oldCategory = await UnitOfWork.CategoryReadRepository.GetByIdAsync(updateCategoryDto.CategoryId, true);

            Mapper.Map(updateCategoryDto, oldCategory);
            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, new CategoryUpdatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(oldCategory),
                CategoryId = categoryId.ToString(),
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

        public async Task<IBaseResult> ChangeStatusAsync(Guid categoryId, bool isActive)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            var category = await UnitOfWork.CategoryReadRepository.GetByIdAsync(categoryId, false);
            category!.IsActive = isActive;

            UnitOfWork.CategoryWriteRepository.Update(category);

            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, new CategoryUpdatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(category),
                CategoryId = categoryId.ToString(),
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IBaseResult> ChangeStatusAsync(Guid categoryId)
        {
            await _businessRules.CheckCategoryExist(categoryId);

            var category = await UnitOfWork.CategoryReadRepository.GetByIdAsync(categoryId, false);
            category!.IsActive = !category.IsActive;

            UnitOfWork.CategoryWriteRepository.Update(category);

            await UnitOfWork.SaveChangesAsync();

            Publisher.Publish(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, new CategoryUpdatedEvent()
            {
                IndexName = ElasticIndexes.CategoryIndex,
                Model = JsonSerializer.Serialize(category),
                CategoryId = categoryId.ToString(),
            });

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }
    }
}
