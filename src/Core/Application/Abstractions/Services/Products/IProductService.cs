using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Products;

namespace Application.Abstractions.Services.Products
{
    public interface IProductService
    {
        Task<IBaseResult> CreateAsync(CreateProductDto createProductDto);
        Task<IBaseResult> UpdateAsync(string productId, UpdateProductDto updateProductDto);
        Task<IBaseResult> RemoveAsync(string productId);
        Task<IDataResult<ProductInfoDto>> GetByIdAsync(string productId);
        Task<IPaginatedDataResult<ProductItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);

    }
}
