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
    public class TemplateContext : IdentityDbContext<User, Role, Guid>
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
            //var hashedPassword = hasher.HashPassword(null, "12345");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("3d8c4bc4-2ee2-4e32-a71d-b7608dfd2cc1"),
                    Email = "batuhan@inal.com",
                    NormalizedEmail = "BATUHAN@INAL.COM",
                    NormalizedUserName = "BATUHAN@INAL.COM",
                    FirstName = "Batuhan",
                    LastName = "Inal",
                    UserName = "batuhan@inal.com",
                    PhoneNumber = "5555555555",
                    PasswordHash = hasher.HashPassword(null, "12345"),
                    SecurityStamp = new Guid().ToString(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                },

            new User
            {
                Id = Guid.Parse("bbb45ad0-9430-4424-b2ce-a16c0af61356"),
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                PhoneNumber = "5414701691",
                PasswordHash = hasher.HashPassword(null, "12345"),
                SecurityStamp = new Guid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            }
                );

            builder.Entity<Role>().HasData(
                    new Role()
                    {
                        Id = Guid.Parse("4e7d13b4-57a9-4775-b849-fddadc2e0ccd"),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = new Guid().ToString(),
                    },
                    new Role()
                    {
                        Id = Guid.Parse("14d485aa-b9e9-45d6-81ed-d9db14197775"),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = new Guid().ToString(),
                    }
                );

            builder.Entity<UserRole>().HasData(
                    new UserRole()
                    {
                        RoleId = Guid.Parse("4e7d13b4-57a9-4775-b849-fddadc2e0ccd"),
                        UserId = Guid.Parse("bbb45ad0-9430-4424-b2ce-a16c0af61356")
                    },
                    new UserRole()
                    {
                        RoleId = Guid.Parse("b6c11f5c-0b65-4872-aa89-3a1c54b101be"),
                        UserId = Guid.Parse("3d8c4bc4-2ee2-4e32-a71d-b7608dfd2cc1")
                    }
                );


            builder.ConfigureCategoryMap();
            builder.ConfigureProductMap();
        }
    }
}
