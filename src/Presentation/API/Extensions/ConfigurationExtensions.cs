using Application.Models.DTOs.Commons.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }

                });
            });
        }

        public static void ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!)),
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    NameClaimType = ClaimTypes.Name,
                };
            });
        }

        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResultDto(context.HttpContext.Response.StatusCode, $"Please try again after {retryAfter.TotalSeconds} second(s).")));
                    }
                    else
                    {
                        await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResultDto(context.HttpContext.Response.StatusCode, $"Please try again after {retryAfter.TotalSeconds} second(s).")));
                    }
                };

                options.AddFixedWindowLimiter("Api", cfgOptions =>
                {
                    cfgOptions.AutoReplenishment = true;
                    cfgOptions.PermitLimit = 60;
                    cfgOptions.Window = TimeSpan.FromMinutes(1);
                });

                options.AddFixedWindowLimiter("Test", cfgOptions =>
                {
                    cfgOptions.AutoReplenishment = true;
                    cfgOptions.PermitLimit = 900;
                    cfgOptions.Window = TimeSpan.FromMinutes(1);
                });
            });
        }
    }
}
