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

        public async Task CheckOwnArticle(Guid articleId,  Guid writerId)
        {
            bool result = await _repository.AnyAsync(x=> x.Id == articleId && x.WriterId == writerId);
            if (!result)
                throw new BusinessException("Dont have permission this article!");
        }

        public async Task CheckArticleExist(Guid articleId) 
        {
            bool result = await _repository.AnyAsync(x => x.Id == articleId);
            if (!result)
                throw new NotFoundException("Article");
        }

        public async Task CheckArticleAlreadyFavorited(Guid articleId, string userId)
        {
            bool result = await _favoriteReadRepository.AnyAsync(x=> x.ArticleId == articleId && x.UserId == Guid.Parse(userId));

            if (result)
                throw new BusinessException("Article already favorited!");
        }
    }
}
