using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Products;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Products;
using Application.Utilities.Exceptions.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.Commons;

namespace Persistence.Services.Products
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductBusinessRule _businessRule;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) : base(unitOfWork, mapper, cache)
        {
            _businessRule = new ProductBusinessRule(unitOfWork.ProductReadRepository);
        }

        public async Task<IBaseResult> CreateAsync(CreateProductDto createProductDto)
        {
            await UnitOfWork.ProductWriteRepository.CreateAsync(Mapper.Map<Product>(createProductDto)!);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(201);
        }

        public async Task<IBaseResult> RemoveAsync(string productId)
        {
            await _businessRule.CheckProductExist(productId);

            await UnitOfWork.ProductWriteRepository.RemoveAsync(productId);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<ProductItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20) => 
            await UnitOfWork.ProductReadRepository
                .Table
                .Select(x => Mapper.Map<ProductItemDto>(x))
                .ToPaginatedListDtoAsync(pageIndex, pageSize);

        public async Task<IDataResult<ProductInfoDto>> GetByIdAsync(string productId)
        {
            await _businessRule.CheckProductExist(productId);

            Product? product = await UnitOfWork.ProductReadRepository
                .Table
                .Where(x=> x.Id == Guid.Parse(productId))
                .Include(x=> x.Category)
                //.Select(p=> Mapper.Map<ProductInfoDto>(p))
                .FirstOrDefaultAsync();
            return new SuccessDataResultDto<ProductInfoDto>(Mapper.Map<ProductInfoDto>(product!));
        }

        public async Task<IBaseResult> UpdateAsync(string productId, UpdateProductDto updateProductDto)
        {
            if (!updateProductDto.ProductId.Equals(productId))
                throw new BusinessException("Product Id degerleri eslesmemektedir!");

            await _businessRule.CheckProductExist(updateProductDto.ProductId);

            Product oldProduct = (await UnitOfWork.ProductReadRepository.GetByIdAsync(updateProductDto.ProductId, true))!;

            Mapper.Map(updateProductDto, oldProduct);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }
    }
}
