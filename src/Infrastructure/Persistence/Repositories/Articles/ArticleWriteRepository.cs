using Application.Abstractions.Repositories.Articles;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Articles
{
    public class ArticleWriteRepository : WriteRepository<Article>, IArticleWriteRepository
    {
        public ArticleWriteRepository(TemplateContext context) : base(context)
        {
        }
    }
}
