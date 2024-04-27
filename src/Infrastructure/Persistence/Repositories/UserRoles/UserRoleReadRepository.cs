using Application.Abstractions.Repositories.UserRoles;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Commons;

namespace Persistence.Repositories.UserRoles
{
    public class UserRoleReadRepository : ReadRepository<UserRole>, IUserRolesReadRepository
    {
        public UserRoleReadRepository(TemplateContext context) : base(context)
        {
        }
    }
}
