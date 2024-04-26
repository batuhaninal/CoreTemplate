using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Commons.Results;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Persistence.Services.Commons;

namespace Persistence.Services.Categories
{
    public class CategoryService : BaseService,ICategoryService
    {
        private readonly CategoryBusinessRules _businessRules;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _businessRules = new CategoryBusinessRules(unitOfWork);
        }

        public async Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            await _businessRules.CheckTitleDuplicate(createCategoryDto.Title);

            await UnitOfWork.CategoryWriteRepository.CreateAsync(Mapper.Map<Category>(createCategoryDto));

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20) =>
            await UnitOfWork.CategoryReadRepository.Table
            .Select(x => Mapper.Map<CategoryItemDto>(x))
            .ToPaginatedListDtoAsync(pageIndex, pageSize, 200);

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

            return new SuccessResultDto(204);
        }
    }
}
