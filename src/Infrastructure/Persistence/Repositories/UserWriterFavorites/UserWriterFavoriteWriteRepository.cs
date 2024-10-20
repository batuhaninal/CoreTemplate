using Application.Abstractions.Repositories.UserWriterFavorites;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.UserWriterFavorites
{
    public class UserWriterFavoriteWriteRepository : WriteRepository<UserWriterFavorite>, IUserWriterFavoriteWriteRepository
    {
        public UserWriterFavoriteWriteRepository(TemplateContext context) : base(context)
        {
        }
    }
}
