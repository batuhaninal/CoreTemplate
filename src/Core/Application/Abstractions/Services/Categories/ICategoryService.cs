using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Categories;

namespace Application.Abstractions.Services.Categories
{
    public interface ICategoryService
    {
        Task<IBaseResult> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<IBaseResult> UpdateAsync(string categoryId, UpdateCategoryDto updateCategoryDto);
        Task<IBaseResult> RemoveAsync(string id);
        Task<IPaginatedDataResult<CategoryItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);
        Task<IDataResult<CategoryItemDto>> GetByIdAsync(string id);
    }
}
