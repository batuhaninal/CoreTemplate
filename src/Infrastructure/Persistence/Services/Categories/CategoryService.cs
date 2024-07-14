using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Commons.Results;
using Application.Models.MessageBrokers.Events;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly CategoryBusinessRules _businessRules;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService rabbitMQPublisherService) : base(unitOfWork, mapper, cache, rabbitMQPublisherService)
        {
            _businessRules = new CategoryBusinessRules(unitOfWork.CategoryReadRepository);
        }

        public async Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            await _businessRules.CheckTitleDuplicate(createCategoryDto.Title);

            await UnitOfWork.CategoryWriteRepository.CreateAsync(Mapper.Map<Category>(createCategoryDto));

            await UnitOfWork.SaveChangesAsync();

            // Eski cache sistemi
            //await Cache.DeleteAllWithPrefixAsync(CachePrefix.Categories.All);

            RemoveCachePrefixes();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            if(pageIndex < 5 && pageSize == 20)
            {
                string? cacheData = await Cache.GetAsync(CachePrefix.Categories.GetAllWithPagination(pageIndex, pageSize));
                if (!string.IsNullOrEmpty(cacheData))
                    return JsonSerializer.Deserialize<PaginatedListDto<CategoryItemDto>>(cacheData)!;
            }

            PaginatedListDto<CategoryItemDto> data = await UnitOfWork.CategoryReadRepository.Table
            .Select(x => Mapper.Map<CategoryItemDto>(x))
            .ToPaginatedListDtoAsync(pageIndex, pageSize, 200);

            if (pageIndex < 5 && pageSize == 20)
                await Cache.AddAsync(CachePrefix.Categories.GetAllWithPagination(pageIndex, pageSize), data);

            return data;
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(BasePaginationRequestParameter pagination) => 
            await GetAllAsync(pagination.PageIndex, pagination.PageSize);

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

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IBaseResult> UpdateAsync(string categoryId, UpdateCategoryDto updateCategoryDto)
        {
            if (!categoryId.Equals(updateCategoryDto.CategoryId))
                throw new Exception("Category Id degerleri eslesmemektedir!");

            await _businessRules.CheckCategoryExist(updateCategoryDto.CategoryId);

            var oldCategory = await UnitOfWork.CategoryReadRepository.GetByIdAsync(updateCategoryDto.CategoryId, true);

            Mapper.Map(updateCategoryDto, oldCategory);
            await UnitOfWork.SaveChangesAsync();

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.Cache, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Categories.Prefix,
                CachePrefix.Articles.Prefix,
            }));
        }
    }
}
