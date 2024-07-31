using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class ArticleMap
    {
        public static void ConfigureArticleMap(this ModelBuilder builder)
        {
            builder.Entity<Article>(p =>
            {
                p.HasKey(x => x.Id);

                p.ToTable("articles");

                p.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                p.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                p.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                p.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                p.Property(x => x.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(100);

                p.Property(x => x.Content)
                    .HasColumnName("content")
                    .IsRequired()
                    .HasColumnType("text");

                p.Property(x => x.CategoryId)
                    .HasColumnName("category_id")
                    .IsRequired();

                p.Property(x => x.WriterId)
                    .HasColumnName("writer_id")
                    .IsRequired();

                p.HasOne(x => x.Category);

                p.HasOne(x => x.Writer);

                p.HasMany(x => x.ArticleFavorites);


                Article[] articles = new Article[]
               {
                    new Article()
                    {
                        Id = Guid.Parse("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                        CategoryId = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        WriterId = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                        Content = "Content",
                        IsActive = true,
                        Title = "Klavye"
                    },

                    new Article()
                    {
                        Id = Guid.Parse("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                        CategoryId = Guid.Parse("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                        WriterId = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                        Content = "Content",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Title = "C#"
                    },

                    new Article()
                    {
                        Id = Guid.Parse("f4edf481-d457-4e3e-a670-0b52635744df"),
                        CategoryId = Guid.Parse("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                        WriterId = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                        Content = "Content",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsActive = true,
                        Title = "C++"
                    },

                    new Article()
                    {
                        Id = Guid.Parse("5836c09f-c947-4222-9cfb-5f665b83f755"),
                        CategoryId = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                        WriterId = Guid.Parse("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                        Content = "Content",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Title = "Go"
                    },

               };

                builder.Entity<Article>().HasData(articles);

            });
        }
    }
}
