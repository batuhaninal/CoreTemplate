using Application.Abstractions.Commons.Security;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class UserMap
    {
        public static void ConfigureUserMap(this ModelBuilder builder, IHashingService hashingService)
        {
            builder.Entity<User>(c =>
            {
                c.HasKey(x => x.Id);

                c.ToTable("users");

                c.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                c.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                c.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                c.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                c.Property(x => x.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(75);

                c.Property(x => x.LastName)
                    .HasColumnName("last_name")
                    .IsRequired()
                    .HasMaxLength(75);

                c.Property(x => x.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(150);

                c.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

                c.Property(x => x.PasswordSalt)
                    .HasColumnName("password_salt")
                    .IsRequired();

                c.HasMany(x=> x.UserRoles);
            });

            byte[] passwordHash, passwordSalt;

            hashingService.CreatePasswordHash("12345", out passwordHash, out passwordSalt);

            builder.Entity<User>().HasData(new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    FirstName = "Batuhan",
                    LastName = "Inal",
                    Email = "batuhan@inal.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
                new User()
                {
                    Id = Guid.Parse("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    FirstName = "User",
                    LastName = "User",
                    Email = "user@user.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
                new User()
                {
                    Id = Guid.Parse("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    FirstName = "Writer",
                    LastName = "Writer",
                    Email = "writer@writer.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            });
        }
    }
}