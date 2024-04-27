using Adapter.Services.Security;
using Adapter.Services.Tokens;
using Application.Abstractions.Commons.Security;
using Application.Abstractions.Commons.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapter
{
    public static class ServiceRegistration
    {
        public static void BindAdapterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHashingService, HashingService>();
            services.AddSingleton<ITokenService, TokenService>();
        }
    }
}
