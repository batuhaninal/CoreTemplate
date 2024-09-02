using Application.Abstractions.Repositories.Commons;
using Domain.Entities;

namespace Application.Abstractions.Repositories.Articles
{
    public interface IArticleReadRepository : IReadRepository<Article>
    {
        Task<Article?> GetArticleByIdWithTrackingAsync(string articleId);
        Task<Article?> GetArticleByIdWithNoTrackingAsync(string articleId);
        Article? GetArticleByIdWithNoTracking(string articleId);
        Article? GetArticleByIdWithTracking(string articleId);
    }
}
