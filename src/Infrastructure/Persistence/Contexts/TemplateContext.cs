using Application.Abstractions.Commons.Security;
using Domain.Entities;
using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.FluentMappings.PostgreSQL;

namespace Persistence.Contexts
{
    public class TemplateContext : DbContext
    {
        private readonly IHashingService _hashingService;
        public TemplateContext(DbContextOptions options, IHashingService hashingService) : base(options)
        {
            _hashingService = hashingService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Writer> Writers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //var hasher = new PasswordHasher<IdttUser>();

            //builder.ConfigureIdttUserMap(hasher);
            //builder.ConfigureIdttUserClaimMap();
            //builder.ConfigureIdttUserLoginMap();
            //builder.ConfigureIdttUserTokenMap();
            //builder.ConfigureIdttRoleMap();
            //builder.ConfigureIdttRoleClaimMap();
            //builder.ConfigureIdttUserRoleMap();

            builder.ConfigureUserMap(_hashingService);
            builder.ConfigureWriterMap();
            builder.ConfigureRoleMap();
            builder.ConfigureUserRoleMap();

            builder.ConfigureCategoryMap();
            builder.ConfigureArticleMap();
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
