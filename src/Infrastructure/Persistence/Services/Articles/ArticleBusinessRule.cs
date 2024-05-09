using Application.Abstractions.Repositories.Articles;
using Application.Utilities.Exceptions.Commons;

namespace Persistence.Services.Articles
{
    public class ArticleBusinessRule
    {
        private readonly IArticleReadRepository _repository;

        public ArticleBusinessRule(IArticleReadRepository repository)
        {
            _repository = repository;
        }

        public async Task CheckArticleExist(string articleId) 
        {
            bool result = await _repository.AnyAsync(x => x.Id == Guid.Parse(articleId));
            if (!result)
                throw new NotFoundException("Article");
        }
    }
}
