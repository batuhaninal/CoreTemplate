using Application.Abstractions.Repositories.ArticleFavorites;
using Application.Abstractions.Repositories.Articles;
using Application.Utilities.Exceptions.Commons;

namespace Persistence.Services.Articles
{
    public class ArticleBusinessRule
    {
        private readonly IArticleReadRepository _repository;
        private readonly IArticleFavoriteReadRepository _favoriteReadRepository;

        public ArticleBusinessRule(IArticleReadRepository repository, IArticleFavoriteReadRepository favoriteReadRepository)
        {
            _repository = repository;
            _favoriteReadRepository = favoriteReadRepository;
        }

        public async Task CheckArticleExist(string articleId) 
        {
            bool result = await _repository.AnyAsync(x => x.Id == Guid.Parse(articleId));
            if (!result)
                throw new NotFoundException("Article");
        }

        public async Task CheckArticleAlreadyFavorited(string articleId, string userId)
        {
            bool result = await _favoriteReadRepository.AnyAsync(x=> x.ArticleId == Guid.Parse(articleId) && x.UserId == Guid.Parse(userId));

            if (result)
                throw new BusinessException("Article already favorited!");
        }
    }
}
