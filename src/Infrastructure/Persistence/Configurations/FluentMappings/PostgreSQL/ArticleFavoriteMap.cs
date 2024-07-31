using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class ArticleFavoriteMap
    {
        public static void ConfigureArticleFavoriteMap(this ModelBuilder builder)
        {
            builder.Entity<ArticleFavorite>(p =>
            {
                p.HasKey(x => x.Id);

                p.ToTable("article_favorites");

                p.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                p.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                p.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                p.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                p.Property(x => x.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                p.Property(x => x.ArticleId)
                    .HasColumnName("article_id")
                    .IsRequired();

                p.HasOne(x => x.User);

                p.HasOne(x => x.Article);
            });
        }
    }
}
