using Domain.Entities;
using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.FluentMappings.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class TemplateContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public TemplateContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hasher = new PasswordHasher<User>();

            builder.ConfigureUserMap(hasher);
            builder.ConfigureUserClaimMap();
            builder.ConfigureUserLoginMap();
            builder.ConfigureUserTokenMap();
            builder.ConfigureRoleMap();
            builder.ConfigureRoleClaimMap();
            builder.ConfigureUserRoleMap();

            builder.ConfigureCategoryMap();
            builder.ConfigureProductMap();
        }
    }
}
