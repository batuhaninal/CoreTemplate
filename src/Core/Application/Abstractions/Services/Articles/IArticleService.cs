using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters.Articles;
using Application.Models.RequestParameters.Commons;

namespace Application.Abstractions.Services.Articles
{
    public interface IArticleService
    {
        Task<IBaseResult> CreateAsync(CreateArticleDto createArticleDto);
        Task<IBaseResult> UpdateAsync(Guid articleId, UpdateArticleDto updateArticleDto);
        Task<IBaseResult> RemoveAsync(Guid articleId);
        Task<IBaseResult> ChangeStatusAsync(Guid articleId);
        Task<IBaseResult> AddToFavAsync(Guid articleId);
        Task<IBaseResult> CreateFavAsync(Guid articleId, Guid userId);
        Task<IDataResult<ArticleInfoDto>> GetByIdAsync(Guid articleId);
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(ArticleRequestParameter articleRequest);
        Task<IPaginatedDataResult<SearchArticleDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
