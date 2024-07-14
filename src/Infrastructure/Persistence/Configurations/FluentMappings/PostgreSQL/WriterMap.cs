using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class WriterMap
    {
        public static void ConfigureWriterMap(this ModelBuilder builder)
        {
            builder.Entity<Writer>(p =>
            {
                p.HasKey(x => x.Id);

                p.ToTable("writers");

                p.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                p.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                p.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                p.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                p.Property(x => x.Nick)
                    .HasColumnName("nick")
                    .IsRequired()
                    .HasMaxLength(100);

                p.Property(x => x.Level)
                    .HasColumnName("level")
                    .IsRequired();

                p.Property(x => x.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();


                p.HasMany(x => x.Articles);

                p.HasOne(x => x.User);

                builder.Entity<Writer>().HasData(new Writer
                {
                    Id = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                    UserId = Guid.Parse("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                    Nick = "default-user",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Level = 4
                });
            });
        }
    }
}
