using Application.Abstractions.Repositories.ArticleFavorites;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.ArticleFavorites
{
    public class ArticleFavoriteReadRepository : ReadRepository<ArticleFavorite>, IArticleFavoriteReadRepository
    {
        public ArticleFavoriteReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
