using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.OpenApi
{
    public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo()
                {
                    Title = $"Core Template Api v{item.ApiVersion}",
                    Version = item.ApiVersion.ToString(),
                    Description = "Core Template Web API",
                    TermsOfService = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
                    Contact = new OpenApiContact() { Name = "Batuhan Inal", Email = "npbatukan@gmail.com", Url = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ") }
                };

                options.SwaggerDoc(item.GroupName, openApiInfo);
            }
        }

        public void Configure(SwaggerGenOptions options)
        {
            Configure(null, options);
        }
    }
}
