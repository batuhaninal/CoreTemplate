using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Articles;
using Application.Models.DTOs.Articles;
using Application.Models.DTOs.Commons.Results;
using Application.Utilities.Exceptions.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.Commons;

namespace Persistence.Services.Articles
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleBusinessRule _businessRule;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) : base(unitOfWork, mapper, cache)
        {
            _businessRule = new ArticleBusinessRule(unitOfWork.ArticleReadRepository);
        }

        public async Task<IBaseResult> CreateAsync(CreateArticleDto createArticleDto)
        {
            await UnitOfWork.ArticleWriteRepository.CreateAsync(Mapper.Map<Article>(createArticleDto)!);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(201);
        }

        public async Task<IBaseResult> RemoveAsync(string articleId)
        {
            await _businessRule.CheckArticleExist(articleId);

            await UnitOfWork.ArticleWriteRepository.RemoveAsync(articleId);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20) => 
            await UnitOfWork.ArticleReadRepository
                .Table
                .Select(x => Mapper.Map<ArticleItemDto>(x))
                .ToPaginatedListDtoAsync(pageIndex, pageSize);

        public async Task<IDataResult<ArticleInfoDto>> GetByIdAsync(string articleId)
        {
            await _businessRule.CheckArticleExist(articleId);

            Article? article = await UnitOfWork.ArticleReadRepository
                .Table
                .Where(x=> x.Id == Guid.Parse(articleId))
                .Include(x=> x.Category)
                //.Select(p=> Mapper.Map<ProductInfoDto>(p))
                .FirstOrDefaultAsync();
            return new SuccessDataResultDto<ArticleInfoDto>(Mapper.Map<ArticleInfoDto>(article!));
        }

        public async Task<IBaseResult> UpdateAsync(string articleId, UpdateArticleDto updateArticleDto)
        {
            if (!updateArticleDto.ArticleId.Equals(articleId))
                throw new BusinessException("Article Id degerleri eslesmemektedir!");

            await _businessRule.CheckArticleExist(updateArticleDto.ArticleId);

            Article oldArticle = (await UnitOfWork.ArticleReadRepository.GetByIdAsync(updateArticleDto.ArticleId, true))!;

            Mapper.Map(updateArticleDto, oldArticle);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(204);
        }
    }
}
