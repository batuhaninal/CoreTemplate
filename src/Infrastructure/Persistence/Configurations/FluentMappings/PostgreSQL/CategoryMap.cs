using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class CategoryMap
    {
        public static void ConfigureCategoryMap(this ModelBuilder builder)
        {
            builder.Entity<Category>(c =>
            {
                c.HasKey(x => x.Id);

                c.ToTable("categories");

                c.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

                c.Property(x => x.ParentId)
                    .HasColumnName("parent_id")
                    .IsRequired(false);


                c.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                c.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                c.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                c.Property(x => x.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(100);

                c.HasMany(m => m.Articles);

                c.HasOne(c=> c.Parent)
                    .WithMany(p=> p.Childrens)
                    .HasForeignKey(c=> c.ParentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            Category[] categories =
            {
                new()
                {
                    Id = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                    Title = "Teknoloji",
                    ParentId = null,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.Parse("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                    Title = "Yazılım",
                    ParentId = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
            };

            builder.Entity<Category>().HasData(categories);
        }
    }
}
