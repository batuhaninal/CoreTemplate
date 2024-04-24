using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Commons.Results;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _businessRules = new CategoryBusinessRules(unitOfWork);
            _mapper = mapper;
        }

        public async Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            await _businessRules.CheckTitleDuplicate(createCategoryDto.Title);

            await _unitOfWork.CategoryWriteRepository.CreateAsync(_mapper.Map<Category>(createCategoryDto));

            await _unitOfWork.SaveChangesAsync();

            return new SuccessResultDto(201);
        }

        public async Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20) =>
            await _unitOfWork.CategoryReadRepository.Table
            .Select(x => _mapper.Map<CategoryItemDto>(x))
            .ToPaginatedListDtoAsync(pageIndex, pageSize, 200);

        public async Task<IDataResult<CategoryItemDto>> GetByIdAsync(string id)
        {
            await _businessRules.CheckCategoryExist(id);

            var category = await _unitOfWork.CategoryReadRepository.GetByIdAsync(id);

            return new SuccessDataResultDto<CategoryItemDto>(_mapper.Map<CategoryItemDto>(category)!, "Urun bulundu");
        }

        public async Task<IBaseResult> RemoveAsync(string id)
        {
            await _businessRules.CheckCategoryExist(id);

            await _unitOfWork.CategoryWriteRepository.RemoveAsync(id);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }

        public async Task<IBaseResult> UpdateAsync(string categoryId, UpdateCategoryDto updateCategoryDto)
        {
            if (!categoryId.Equals(updateCategoryDto.CategoryId))
                throw new Exception("Category Id degerleri eslesmemektedir!");

            await _businessRules.CheckCategoryExist(updateCategoryDto.CategoryId);

            var oldCategory = await _unitOfWork.CategoryReadRepository.GetByIdAsync(updateCategoryDto.CategoryId, true);

            _mapper.Map(updateCategoryDto, oldCategory);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }
    }
}
