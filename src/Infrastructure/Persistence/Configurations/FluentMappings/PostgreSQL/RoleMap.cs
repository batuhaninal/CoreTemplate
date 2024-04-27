using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class RoleMap
    {
        public static void ConfigureRoleMap(this ModelBuilder builder)
        {
            builder.Entity<Role>(c =>
            {
                c.HasKey(x => x.Id);

                c.ToTable("roles");

                c.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                c.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                c.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                c.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                c.Property(x => x.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(75);

                c.HasMany(x=> x.UserRoles);
            });

            builder.Entity<Role>().HasData(new List<Role>()
            {
                new Role()
                {
                    Id = Guid.Parse("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "Admin"
                },
                new Role()
                {
                    Id = Guid.Parse("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "User"
                },
                new Role()
                {
                    Id = Guid.Parse("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "Seller"
                }
            });
        }
    }
}
