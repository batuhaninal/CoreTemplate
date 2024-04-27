using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.FluentMappings.PostgreSQL
{
    public static class ProductMap
    {
        public static void ConfigureProductMap(this ModelBuilder builder)
        {
            builder.Entity<Product>(p =>
            {
                p.HasKey(x => x.Id);

                p.ToTable("products");

                p.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();


                p.Property(c => c.CreatedDate).HasColumnName("created_date").IsRequired();
                p.Property(c => c.UpdatedDate).HasColumnName("updated_date").IsRequired();
                p.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

                p.Property(x => x.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(100);

                p.Property(x => x.CategoryId)
                    .HasColumnName("category_id")
                    .IsRequired();

                p.Property(x => x.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                p.HasOne(x => x.Category);


                Product[] products = new Product[]
               {
                    new Product()
                    {
                        Id = Guid.Parse("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                        CategoryId = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Price = 120,
                        IsActive = true,
                        Title = "Klavye"
                    },

                    new Product()
                    {
                        Id = Guid.Parse("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                        CategoryId = Guid.Parse("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                        Price = 250,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Title = "Tyler Durden Poları"
                    },

                    new Product()
                    {
                        Id = Guid.Parse("f4edf481-d457-4e3e-a670-0b52635744df"),
                        CategoryId = Guid.Parse("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                        Price = 300,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsActive = true,
                        Title = "Zen"
                    },

                    new Product()
                    {
                        Id = Guid.Parse("5836c09f-c947-4222-9cfb-5f665b83f755"),
                        CategoryId = Guid.Parse("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                        Price = 400,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Title = "Klavye"
                    },

               };

                builder.Entity<Product>().HasData(products);

            });
        }
    }
}
