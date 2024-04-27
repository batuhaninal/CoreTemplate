using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class IdentityMap
    {
        public static void ConfigureIdttUserMap(this ModelBuilder builder, PasswordHasher<IdttUser> hasher)
        {
            builder.Entity<IdttUser>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasName("user_name_index").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("email_index");

                // Maps to the AspNetUsers table
                b.ToTable("asp_net_users");

                b.Property(u=> u.Id).HasColumnName("id");
                b.Property(u=> u.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
                b.Property(u=> u.LastName).HasColumnName("last_name").HasMaxLength(120).IsRequired();
                b.Property(u=> u.PhoneNumber).HasColumnName("phone_number").IsRequired();
                b.Property(u=> u.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed").IsRequired();
                b.Property(u=> u.RefreshToken).HasColumnName("refresh_token").HasMaxLength(256).IsRequired(false);

                b.Property(u=> u.EmailConfirmed).HasColumnName("email_confirmed");
                b.Property(u=> u.PasswordHash).HasColumnName("password_hash");
                b.Property(u=> u.SecurityStamp).HasColumnName("security_stamp");
                b.Property(u=> u.TwoFactorEnabled).HasColumnName("two_factor_enabled");
                b.Property(u=> u.LockoutEnd).HasColumnName("lockout_end");
                b.Property(u=> u.LockoutEnabled).HasColumnName("lockout_enabled");
                b.Property(u=> u.AccessFailedCount).HasColumnName("access_failed_count");

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
                b.HasMany<IdttUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<IdttUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<IdttUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<IdttUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<IdttUser>().HasData(
                new IdttUser
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
                new IdttUser
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

        public static void ConfigureIdttUserClaimMap(this ModelBuilder builder)
        {
            builder.Entity<IdttUserClaim>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);
                
                b.Property(u=> u.Id).HasColumnName("id");

                b.Property(u=> u.UserId).HasColumnName("user_id");

                b.Property(u=> u.ClaimType).HasColumnName("claim_type");

                b.Property(u=> u.ClaimValue).HasColumnName("claim_value");

                // Maps to the AspNetUserClaims table
                b.ToTable("asp_net_user_claims");
            });
        }

        public static void ConfigureIdttUserLoginMap(this ModelBuilder builder)
        {
            builder.Entity<IdttUserLogin>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider)
                    .HasColumnName("login_provider")
                    .HasMaxLength(128);

                b.Property(l => l.ProviderKey)
                    .HasColumnName("provider_key")
                    .HasMaxLength(128);

                b.Property(l => l.ProviderDisplayName)
                    .HasColumnName("provider_display_name");

                b.Property(l => l.UserId)
                   .HasColumnName("user_id");

                // Maps to the AspNetUserLogins table
                b.ToTable("asp_net_user_logins");
            });
        }

        public static void ConfigureIdttUserTokenMap(this ModelBuilder builder)
        {
            builder.Entity<IdttUserToken>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider)
                    .HasColumnName("login_provider")
                    .HasMaxLength(256);

                b.Property(t => t.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256);

                b.Property(t=> t.UserId)
                    .HasColumnName("user_id");

                b.Property(t=> t.Value)
                    .HasColumnName("value");

                // Maps to the AspNetUserTokens table
                b.ToTable("asp_net_user_tokens");
            });
        }

        public static void ConfigureIdttRoleMap(this ModelBuilder builder)
        {
            builder.Entity<IdttRole>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                b.Property(r => r.Id).HasColumnName("id");

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("asp_net_roles");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(r => r.ConcurrencyStamp).HasColumnName("concurrency_stamp");

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256);

                b.Property(u => u.NormalizedName).HasColumnName("normalized_name").HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<IdttUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<IdttRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<IdttRole>().HasData(
                    new IdttRole()
                    {
                        Id = Guid.Parse("4e7d13b4-57a9-4775-b849-fddadc2e0ccd"),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = new Guid().ToString(),
                    },
                    new IdttRole()
                    {
                        Id = Guid.Parse("14d485aa-b9e9-45d6-81ed-d9db14197775"),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = new Guid().ToString(),
                    }
                );
        }

        public static void ConfigureIdttRoleClaimMap(this ModelBuilder builder)
        {
            builder.Entity<IdttRoleClaim>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                b.Property(r => r.Id).HasColumnName("id");

                b.Property(r => r.RoleId).HasColumnName("role_id");

                b.Property(r => r.ClaimType).HasColumnName("claim_type");

                b.Property(r => r.ClaimValue).HasColumnName("claim_value");

                // Maps to the AspNetRoleClaims table
                b.ToTable("asp_net_role_claims");
            });
        }

        public static void ConfigureIdttUserRoleMap(this ModelBuilder builder)
        {
            builder.Entity<IdttUserRole>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                b.Property(x => x.UserId).HasColumnName("user_id");
                b.Property(x => x.RoleId).HasColumnName("role_id");

                // Maps to the AspNetUserRoles table
                b.ToTable("asp_net_user_roles");
            });

            builder.Entity<IdttUserRole>().HasData(
                    new IdttUserRole()
                    {
                        RoleId = Guid.Parse("4e7d13b4-57a9-4775-b849-fddadc2e0ccd"),
                        UserId = Guid.Parse("bbb45ad0-9430-4424-b2ce-a16c0af61356")
                    },
                    new IdttUserRole()
                    {
                        RoleId = Guid.Parse("14d485aa-b9e9-45d6-81ed-d9db14197775"),
                        UserId = Guid.Parse("3d8c4bc4-2ee2-4e32-a71d-b7608dfd2cc1")
                    }
                );
        }
    }
}
