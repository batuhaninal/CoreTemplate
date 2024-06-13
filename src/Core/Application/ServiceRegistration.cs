using Application.Models.Constants.Options;
using Application.Utilities.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void BindApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CategoryProfile));

            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));

            // AddControllers'dan sonra cagrilmasi gerekiyor
            //services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
        }
    }
}
