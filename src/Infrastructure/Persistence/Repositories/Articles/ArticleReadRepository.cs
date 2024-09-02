using Application.Abstractions.Repositories.Articles;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Articles
{
    public class ArticleReadRepository : ReadRepository<Article>, IArticleReadRepository
    {
        private readonly TemplateContext _templateContext;
        public ArticleReadRepository(TemplateContext context) : base(context)
        {
            _templateContext = context;
        }

        private static Func<TemplateContext, Guid, Task<Article?>> GetByIdWithNoTrackingCQAsync =
            EF.CompileAsyncQuery((TemplateContext context, Guid articleId) =>
                context.Set<Article>()
                .AsNoTracking()
                .FirstOrDefault(x=> x.Id == articleId)
            );

        private static Func<TemplateContext, Guid, Task<Article?>> GetByIdWithTrackingCQAsync =
            EF.CompileAsyncQuery((TemplateContext context, Guid articleId) =>
                context.Set<Article>()
                .FirstOrDefault(x => x.Id == articleId)
            );

        private static Func<TemplateContext, Guid, Article?> GetByIdWithNoTrackingCQ =
            EF.CompileQuery((TemplateContext context, Guid articleId) =>
                context.Set<Article>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == articleId)
            );

        private static Func<TemplateContext, Guid, Article?> GetByIdWithTrackingCQ =
            EF.CompileQuery((TemplateContext context, Guid articleId) =>
                context.Set<Article>()
                .FirstOrDefault(x => x.Id == articleId)
            );

        public async Task<Article?> GetArticleByIdWithNoTrackingAsync(string articleId) =>
            await GetByIdWithNoTrackingCQAsync(_templateContext, Guid.Parse(articleId));

        public async Task<Article?> GetArticleByIdWithTrackingAsync(string articleId) =>
            await GetByIdWithTrackingCQAsync(_templateContext, Guid.Parse(articleId));

        public Article? GetArticleByIdWithNoTracking(string articleId) =>
             GetByIdWithNoTrackingCQ(_templateContext, Guid.Parse(articleId));

        public Article? GetArticleByIdWithTracking(string articleId) =>
            GetByIdWithTrackingCQ(_templateContext, Guid.Parse(articleId));


    }
}
