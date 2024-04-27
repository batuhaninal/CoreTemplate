using Domain.Entities;
using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });
        }
    }
}
