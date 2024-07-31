using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Articles;

namespace Application.Abstractions.Services.Articles
{
    public interface IArticleService
    {
        Task<IBaseResult> CreateAsync(CreateArticleDto createArticleDto);
        Task<IBaseResult> UpdateAsync(string articleId, UpdateArticleDto updateArticleDto);
        Task<IBaseResult> RemoveAsync(string articleId);
        Task<IBaseResult> Fav(string articleId, string userId);
        Task<IDataResult<ArticleInfoDto>> GetByIdAsync(string articleId);
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);
    }
}
