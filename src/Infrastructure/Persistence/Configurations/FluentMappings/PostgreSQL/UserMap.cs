using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class UserMap
    {
        public static void ConfigureUserMap(this ModelBuilder builder)
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

                c.HasMany<UserRole>().WithOne().HasForeignKey(ur=> ur.UserId).IsRequired();
            });
        }
    }
}