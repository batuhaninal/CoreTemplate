using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class UserRoleMap
    {
        public static void ConfigureUserRoleMap(this ModelBuilder builder)
        {
            builder.Entity<UserRole>(c =>
            {
                c.HasKey(x => x.Id);

                c.ToTable("user_roles");

                c.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                c.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                c.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                c.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                c.Property(x => x.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                c.Property(x => x.RoleId)
                    .HasColumnName("role_id")
                    .IsRequired();

                c.HasOne(x => x.User);
                c.HasOne(x => x.Role);
            });

            builder.Entity<UserRole>().HasData(new List<UserRole>()
            {
                new UserRole()
                {
                    Id = Guid.Parse("0e398d5d-c49e-4b68-8da1-9616a0145a6d\r\n"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    UserId = Guid.Parse("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                    RoleId = Guid.Parse("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b")
                },
                new UserRole()
                {
                    Id = Guid.Parse("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    UserId = Guid.Parse("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                    RoleId = Guid.Parse("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2")
                },
                new UserRole()
                {
                    Id = Guid.Parse("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    UserId = Guid.Parse("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                    RoleId = Guid.Parse("83e5c9f0-7e6d-4a08-a515-2e8889f3b140")
                }
            });
        }
    }
}
