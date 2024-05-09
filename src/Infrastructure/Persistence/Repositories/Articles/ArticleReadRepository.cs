using Application.Abstractions.Repositories.Articles;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Articles
{
    public class ArticleReadRepository : ReadRepository<Article>, IArticleReadRepository
    {
        public ArticleReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
