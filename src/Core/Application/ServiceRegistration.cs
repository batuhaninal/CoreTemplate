using Application.Models.Constants.Options;
using Application.Utilities.MappingProfiles;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Utilities.FluentValidations.Categories;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void BindApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CategoryProfile));

            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();

            // AddControllers'dan sonra cagrilmasi gerekiyor
            //services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
        }
    }
}
