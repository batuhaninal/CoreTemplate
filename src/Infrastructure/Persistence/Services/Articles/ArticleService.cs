using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Articles;
using Application.Models.Constants.CachePrefixes;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Articles;
using Application.Models.DTOs.Commons.Results;
using Application.Models.MessageBrokers.Events;
using Application.Models.MessageBrokers.Events.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Exceptions.Commons;
using Application.Utilities.Pagination;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Articles.Extensions;
using Persistence.Services.Commons;
using System.Text.Json;

namespace Persistence.Services.Articles
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleBusinessRule _businessRule;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService rabbitMQPublisherService) : base(unitOfWork, mapper, cache, rabbitMQPublisherService)
        {
            _businessRule = new ArticleBusinessRule(unitOfWork.ArticleReadRepository, unitOfWork.ArticleFavoriteReadRepository);
        }

        public async Task<IBaseResult> CreateAsync(CreateArticleDto createArticleDto)
        {
            await UnitOfWork.ArticleWriteRepository.CreateAsync(Mapper.Map<Article>(createArticleDto)!);
            await UnitOfWork.SaveChangesAsync();

            // Eski cache sistemi
            //await Cache.DeleteAllWithPrefixAsync(CachePrefix.Articles.All);

            RemoveCachePrefixes();

            return new SuccessResultDto(201);
        }

        public async Task<IBaseResult> RemoveAsync(string articleId)
        {
            await _businessRule.CheckArticleExist(articleId);

            await UnitOfWork.ArticleWriteRepository.RemoveAsync(articleId);

            await UnitOfWork.SaveChangesAsync();

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20)
        {
            if (pageIndex < 5 && pageSize == 20)
            {
                string? cacheData = await Cache.GetAsync(CachePrefix.Articles.GetAllWithPagination(pageIndex, pageSize));
                if (!string.IsNullOrEmpty(cacheData))
                    return JsonSerializer.Deserialize<PaginatedListDto<ArticleItemDto>>(cacheData)!;
            }

            var data = await UnitOfWork.ArticleReadRepository
                .Table
                .Include(x=> x.Category)
                .Include(x=> x.Writer)
                    .ThenInclude(w=> w!.User)
                .Select(x => Mapper.Map<ArticleItemDto>(x))
                .ToPaginatedListDtoAsync(pageIndex, pageSize);

            if (pageIndex < 5 && pageSize == 20)
                await Cache.AddAsync(CachePrefix.Articles.GetAllWithPagination(pageIndex, pageSize), data);

            return data;
        }

        public async Task<IDataResult<ArticleInfoDto>> GetByIdAsync(string articleId)
        {
            await _businessRule.CheckArticleExist(articleId);

            ArticleInfoDto article = (await UnitOfWork.ArticleReadRepository
                .Table
                .Where(x => x.Id == Guid.Parse(articleId))
                .Include(x => x.Category)
                .Include(x=> x.Writer)
                    .ThenInclude(w=> w.User)
                .Select(p=> Mapper.Map<ArticleInfoDto>(p))
                .FirstOrDefaultAsync())!;

            return new SuccessDataResultDto<ArticleInfoDto>(article);
        }

        public async Task<IBaseResult> UpdateAsync(string articleId, UpdateArticleDto updateArticleDto)
        {
            if (!updateArticleDto.ArticleId.Equals(articleId))
                throw new BusinessException("Article Id degerleri eslesmemektedir!");

            await _businessRule.CheckArticleExist(updateArticleDto.ArticleId);

            Article oldArticle = (await UnitOfWork.ArticleReadRepository.GetByIdAsync(updateArticleDto.ArticleId, true))!;

            Mapper.Map(updateArticleDto, oldArticle);

            await UnitOfWork.SaveChangesAsync();

            RemoveCachePrefixes();

            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(BasePaginationRequestParameter pagination) =>
            await GetAllAsync(pagination.PageIndex, pagination.PageSize);

        private void RemoveCachePrefixes()
        {
            Publisher.Publish(QueueNames.CacheRemove, ExchangeNames.Cache, new CacheRemovedEvent(new string[]
            {
                CachePrefix.Articles.Prefix,
            }));
        }

        public async Task<IBaseResult> Fav(string articleId, string userId)
        {
            await _businessRule.CheckArticleAlreadyFavorited(articleId, userId);
            Publisher.Publish(QueueNames.ArticleLike, ExchangeNames.Article, new ArticleFavoritedEvent() { ArticleId = Guid.Parse(articleId), UserId = Guid.Parse(userId) });
            return new SuccessResultDto(204);
        }

        public async Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(ArticleRequestParameter articleRequest, PaginationRequestParameter pagination)
        {
            /* (.ProjectTo<ArticleItemDto>(Mapper.ConfigurationProvider)) Buyuk data sorgulari icin performansli fakat kucuk veriler icin performansi dusuk!  Query ciktisi =>
             SELECT t.title, FALSE, c.id::text, c.title, c.created_date, FALSE, w.nick, w.level::smallint, w.id::text, t.created_date, t.id::text
                FROM (
                    SELECT a.id, a.category_id, a.created_date, a.title, a.writer_id
                    FROM articles AS a
                    LIMIT @__p_1 OFFSET @__p_0
                ) AS t
            INNER JOIN categories AS c ON t.category_id = c.id
            INNER JOIN writers AS w ON t.writer_id = w.id
            */

            //var data = await UnitOfWork.ArticleReadRepository
            //    .Table
            //    .Include(x => x.Category)
            //    .Include(x=> x.Writer)
            //        .ThenInclude(w=> w!.User)
            //    .Filter(articleRequest)
            //    .ProjectTo<ArticleItemDto>(Mapper.ConfigurationProvider)
            //    .ToPaginatedListDtoAsync(pagination);


            /* (.Select(x => Mapper.Map<ArticleItemDto>(x))) Buyuk data sorgulari icin perfonmansi dusuk fakat kucuk veriler icin cok daha performansli! Query ciktisi =>
             SELECT t.id, t.category_id, t.content, t.created_date, t.is_active, t.title, t.updated_date, t.writer_id, c.id, c.created_date, c.is_active, c.title, c.updated_date, w.id, w.created_date, w.is_active, w.level, w.nick, w.updated_date, w.user_id
                FROM (
                    SELECT a.id, a.category_id, a.content, a.created_date, a.is_active, a.title, a.updated_date, a.writer_id
                    FROM articles AS a
                    LIMIT @__p_2 OFFSET @__p_1
                ) AS t
            INNER JOIN categories AS c ON t.category_id = c.id
            INNER JOIN writers AS w ON t.writer_id = w.id
             */

            var data = await UnitOfWork.ArticleReadRepository
                .Table
                .Include(x => x.Category)
                .Include(x=> x.Writer)
                    .ThenInclude(w=> w!.User)
                .Filter(articleRequest)
                .Select(x => Mapper.Map<ArticleItemDto>(x))
                .ToPaginatedListDtoAsync(pagination);

            return data;
        }
    }
}
