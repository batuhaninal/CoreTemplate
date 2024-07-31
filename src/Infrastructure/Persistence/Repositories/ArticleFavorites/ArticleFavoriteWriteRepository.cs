using Application.Abstractions.Repositories.ArticleFavorites;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.ArticleFavorites
{
    public class ArticleFavoriteWriteRepository : WriteRepository<ArticleFavorite>, IArticleFavoriteWriteRepository
    {
        public ArticleFavoriteWriteRepository(TemplateContext context) : base(context)
        {
        }
    }
}
