using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class IdentityMap
    {
        public static void ConfigureUserMap(this ModelBuilder builder, PasswordHasher<User> hasher)
        {
            builder.Entity<User>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("user_name_index").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("email_index");

                // Maps to the AspNetUsers table
                b.ToTable("asp_net_users");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasColumnName("user_name").HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasColumnName("normalized_user_name").HasMaxLength(256);
                b.Property(u => u.Email).HasColumnName("email").HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(256);

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

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
                });
        }

        public static void ConfigureUserClaimMap(this ModelBuilder builder)
        {
            builder.Entity<UserClaim>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("asp_net_user_claims");
            });
        }

        public static void ConfigureUserLoginMap(this ModelBuilder builder)
        {
            builder.Entity<UserLogin>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Maps to the AspNetUserLogins table
                b.ToTable("asp_net_user_logins");
            });
        }

        public static void ConfigureUserTokenMap(this ModelBuilder builder)
        {
            builder.Entity<UserToken>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider).HasMaxLength(256);
                b.Property(t => t.Name).HasMaxLength(256);

                // Maps to the AspNetUserTokens table
                b.ToTable("asp_net_user_tokens");
            });
        }

        public static void ConfigureRoleMap(this ModelBuilder builder)
        {
            builder.Entity<Role>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("asp_net_roles");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

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
        }

        public static void ConfigureRoleClaimMap(this ModelBuilder builder)
        {
            builder.Entity<RoleClaim>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("asp_net_role_claims");
            });
        }

        public static void ConfigureUserRoleMap(this ModelBuilder builder)
        {
            builder.Entity<UserRole>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                b.Property(x => x.UserId).HasColumnName("user_id");
                b.Property(x => x.RoleId).HasColumnName("role_id");

                // Maps to the AspNetUserRoles table
                b.ToTable("asp_net_user_roles");
            });

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
        }
    }
}
