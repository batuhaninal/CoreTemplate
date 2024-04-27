using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Categories;
using Application.Abstractions.Services.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Commons;
using Persistence.Services.Categories;
using Persistence.Services.Products;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void BindPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateContext>(options => options.UseNpgsql(configuration.GetConnectionString("PgSQL")));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            // Unit of Work design pattern oncesi repository bagimliliklari icin servis kaydik yapmamiz gerekli!
            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            //services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
