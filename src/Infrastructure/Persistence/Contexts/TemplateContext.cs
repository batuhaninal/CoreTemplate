using Domain.Entities;
using Domain.Entities.Commons;
using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.FluentMappings.PostgreSQL;

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

        // Dto mapping isleminde de handle edilebilir state durumlari
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in data)
            {
                _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
