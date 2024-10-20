using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class UserWriterFavoriteMap
    {
        public static void ConfigureUserWriterFavoriteMap(this ModelBuilder builder)
        {
            builder.Entity<UserWriterFavorite>(p =>
            {
                p.HasKey(x => x.Id);

                p.ToTable("user_writer_favorites");

                p.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                p.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                p.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                p.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                p.Property(x => x.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                p.Property(x => x.WriterId)
                    .HasColumnName("writer_id")
                    .IsRequired();

                p.HasOne(x => x.User);

                p.HasOne(x => x.Writer);
            });
        }
    }
}
