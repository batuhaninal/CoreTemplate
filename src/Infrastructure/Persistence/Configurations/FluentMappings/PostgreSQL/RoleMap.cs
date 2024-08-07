﻿using Application.Models.Constants.Roles;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
                    Id = Guid.Parse(AppRoles.Admin),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "admin"
                },
                new Role()
                {
                    Id = Guid.Parse(AppRoles.User),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "user"
                },
                new Role()
                {
                    Id = Guid.Parse(AppRoles.Writer),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Name = "writer"
                }
            });
        }
    }
}
