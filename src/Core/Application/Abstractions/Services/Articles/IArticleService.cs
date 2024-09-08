using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters;
using Application.Models.RequestParameters.Articles;
using Domain.Entities;

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
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(ArticleRequestParameter articleRequest, PaginationRequestParameter pagination);
        Task<IDataResult<IList<SearchArticleDto>>> SearchAsync(string condition, int size = 10);
        Task<Article?> Test1(string articleId);
        Task<Article?> Test2(string articleId);
        Task<Article?> Test3(string articleId);
        Task<Article?> Test4(string articleId);
        Article? Test5(string articleId);
        Article? Test6(string articleId);
        Article? Test7(string articleId);
        Article? Test8(string articleId);
    }
}
