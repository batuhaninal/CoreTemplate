using Application.Utilities.FluentValidations.Categories;
using Application.Utilities.MappingProfiles;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void BindApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CategoryProfile));

            // AddControllers'dan sonra cagrilmasi gerekiyor
            //services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
        }
    }
}
