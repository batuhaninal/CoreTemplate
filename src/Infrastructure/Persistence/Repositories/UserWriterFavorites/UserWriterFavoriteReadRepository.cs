using Application.Abstractions.Repositories.UserWriterFavorites;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.UserWriterFavorites
{
    public class UserWriterFavoriteReadRepository : ReadRepository<UserWriterFavorite>, IUserWriterFavoriteReadRepository
    {
        public UserWriterFavoriteReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
