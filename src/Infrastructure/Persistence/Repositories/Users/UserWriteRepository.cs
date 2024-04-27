using Application.Abstractions.Repositories.Users;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.Users
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(TemplateContext context) : base(context)
        {
        }
    }
}
