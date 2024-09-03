using Application.Models.DTOs.Commons.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Contexts;
using Serilog;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;
using Serilog.Formatting.Elasticsearch;
using Serilog.Core;
using Serilog.Sinks.Elasticsearch;
using Asp.Versioning;

namespace API.Extensions
{
    public static class ConfigurationExtensions
    {

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Core Template v1", Version = "v1", Description = "Core Template Web API", TermsOfService = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ"), Contact = new OpenApiContact() { Name = "Batuhan Inal", Email = "npbatukan@gmail.com", Url = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ") } });
                //options.SwaggerDoc("v2", new OpenApiInfo() { Title = "Core Template v2", Version = "v2", Description = "Core Template Web API" });

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
                            },
                            Name = "Bearer"
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

        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            Logger log = new LoggerConfiguration()
            .WriteTo.Console()
            // Dosyaya yazma
            //.WriteTo.File("logs/log.txt")
            // PostgreSQL log configuration
            //.WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PgSQL"), "logs", needAutoCreateTable: true,
            //columnOptions: new Dictionary<string, ColumnWriterBase>
            //{
            //    { "message", new RenderedMessageColumnWriter() },
            //    { "message_template", new MessageTemplateColumnWriter() },
            //    { "level", new LevelColumnWriter() },
            //    { "time_stamp", new TimestampColumnWriter() },
            //    { "exception", new ExceptionColumnWriter() },
            //    { "log_event", new LogEventSerializedColumnWriter() },
            //    { "user_name", new UsernameColumnWriter() }
            //})
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["Elastic:Url"]!))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                IndexFormat = $"{builder.Configuration["Elastic:IndexName"]!}-{builder.Environment.EnvironmentName}-logs-" + "{0:yyy.MM.dd}",
                ModifyConnectionSettings = x => x.BasicAuthentication(builder.Configuration["Elastic:Username"]!, builder.Configuration["Elastic:Password"]!),
                CustomFormatter = new ElasticsearchJsonFormatter()
            })
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

            builder.Host.UseSerilog(log);
        }

        public static async Task ConfigureMigrationAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TemplateContext>();
            await context.Database.MigrateAsync();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(2);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                            new UrlSegmentApiVersionReader(),
                            new HeaderApiVersionReader("X-Api-Version")
                        );
            })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });
        }
    }
}
