using Application.Abstractions.Repositories.Commons;
using Domain.Entities;

namespace Application.Abstractions.Repositories.Articles
{
    public interface IArticleWriteRepository : IWriteRepository<Article> 
    {
    }
}
