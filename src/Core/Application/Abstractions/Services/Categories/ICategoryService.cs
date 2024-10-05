using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Categories;
using Application.Models.RequestParameters.Categories;
using Application.Models.RequestParameters.Commons;

namespace Application.Abstractions.Services.Categories
{
    public interface ICategoryService
    {
        Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<IBaseResult> UpdateAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        Task<IBaseResult> RemoveAsync(Guid categoryId);
        Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);
        Task<IPaginatedDataResult<CategoryToolDto>> GetAllToolsAsync(int pageIndex = 1, int pageSize = 20);
        Task<IPaginatedDataResult<CategoryToolDto>> GetAllBaseCategoriesAsync(int pageIndex = 1, int pageSize = 20);
        Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(CategoryRequestParameter parameter, BasePaginationRequestParameter pagination);
        Task<IDataResult<CategoryItemDto>> GetByIdAsync(Guid categoryId);
        Task<IPaginatedDataResult<SearchCategoryDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
