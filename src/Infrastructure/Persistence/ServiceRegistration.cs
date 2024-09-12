using Application.Abstractions.Repositories.Articles.Elasticsearch;
using Application.Abstractions.Repositories.Categories.Elasticsearch;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Repositories.Users.Elasticsearch;
using Application.Abstractions.Repositories.Writers.Elasticsearch;
using Application.Abstractions.Services.Articles;
using Application.Abstractions.Services.Auths;
using Application.Abstractions.Services.Categories;
using Application.Abstractions.Services.Users;
using Application.Abstractions.Services.Writers;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Articles.Elasticsearch;
using Persistence.Repositories.Categories.Elasticsearch;
using Persistence.Repositories.Commons;
using Persistence.Repositories.Users.Elasticsearch;
using Persistence.Repositories.Writers.Elasticsearch;
using Persistence.Services.Articles;
using Persistence.Services.Auths;
using Persistence.Services.Categories;
using Persistence.Services.Users;
using Persistence.Services.Writers;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void BindPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateContext>(options => options.UseNpgsql(configuration.GetConnectionString("PgSQL")));

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //});

            // Unit of Work design pattern oncesi repository bagimliliklari icin servis kaydik yapmamiz gerekli!
            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            //services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();


            // Seed data icin
            //var scope = services.BuildServiceProvider();

            //var seedData = new SeedData(scope.GetRequiredService<IHashingService>());

            //await seedData.SeedAsync(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IArticleService, ArticleService>();

            services.AddScoped<IWriterService, WriterService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IELKArticleRepository, ELKArticleRepository>();
            services.AddScoped<IELKCategoryRepository, ELKCategoryRepository>();
            services.AddScoped<IELKWriterRepository, ELKWriterRepository>();
            services.AddScoped<IELKUserRepository, ELKUserRepository>();

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!))
                .Authentication(new BasicAuthentication(configuration.GetSection("Elastic")["Username"]!, configuration.GetSection("Elastic")["Password"]!));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);

            services.AddScoped<IElasticSearchWriteRepository, ElasticSearchWriteRepository>();

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("PgSQL")!);
        }
    }
}
