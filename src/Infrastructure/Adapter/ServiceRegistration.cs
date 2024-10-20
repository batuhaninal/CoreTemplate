using Adapter.Services.Caching;
using Adapter.Services.Files;
using Adapter.Services.MessageBrokers;
using Adapter.Services.MessageBrokers.Consumers.Articles.ELKs;
using Adapter.Services.MessageBrokers.Consumers.Articles.Postgres;
using Adapter.Services.MessageBrokers.Consumers.Caching;
using Adapter.Services.MessageBrokers.Consumers.Categories.ELKs;
using Adapter.Services.MessageBrokers.Consumers.Users.ELKs;
using Adapter.Services.MessageBrokers.Consumers.Writers.ELKs;
using Adapter.Services.MessageBrokers.Consumers.Writers.Postgres;
using Adapter.Services.MessageBrokers.Publishers;
using Adapter.Services.Security;
using Adapter.Services.Tokens;
using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.Files;
using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Security;
using Application.Abstractions.Commons.Tokens;
using Application.Models.Constants.Options;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Adapter
{
    public static class ServiceRegistration
    {
        public static void BindAdapterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHashingService, HashingService>();
            services.AddSingleton<ITokenService, TokenService>();

            services.AddScoped<IUserTokenService, UserTokenService>();

            services.AddSingleton<ICacheService>(serviceProvider =>
            {
                var redisOptions = new RedisCacheOptions
                {
                    ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                    {
                        EndPoints = { { configuration["Redis:Host"]!, int.Parse(configuration["Redis:Port"]!.ToString()) } },
                        Password = configuration["Redis:Password"],
                        DefaultDatabase = int.Parse(configuration["Redis:DbIndex"]!),
                        AbortOnConnectFail = false
                    }
                };

                return new CacheService(redisOptions.ConfigurationOptions);
            });


            // OpenTelemetry Redis intrumentation icin gerekli
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisService = sp.GetService<ICacheService>();

                return redisService.GetConnectionMultiplexer();
            });

            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            services.AddSingleton<IRabbitMQPublisherService, RabbitMQPublisherService>();

            services.AddSingleton<IFileService, LocaleFileService>();

            services.AddHostedService<CacheRemovedEventConsumer>();

            services.AddHostedService<ArticleCreatedEventConsumer>();
            services.AddHostedService<ArticleUpdatedEventConsumer>();
            services.AddHostedService<ArticleRemovedEventConsumer>();

            services.AddHostedService<ArticleFavoritedEventConsumer>();

            services.AddHostedService<CategoryCreatedEventConsumer>();
            services.AddHostedService<CategoryUpdatedEventConsumer>();
            services.AddHostedService<CategoryRemovedEventConsumer>();

            services.AddHostedService<WriterCreatedEventConsumer>();
            services.AddHostedService<WriterUpdatedEventConsumer>();
            services.AddHostedService<WriterRemovedEventConsumer>();

            services.AddHostedService<WriterFavoritedEventConsumer>();

            services.AddHostedService<UserCreatedEventConsumer>();
            services.AddHostedService<UserUpdatedEventConsumer>();
            services.AddHostedService<UserRemovedEventConsumer>();

            RabbitMQOptions rbmq = configuration.GetSection("RabbitMQOptions").Get<RabbitMQOptions>()!;

            services.AddHealthChecks()
                .AddRedis($"{configuration["Redis:Host"]}:{configuration["Redis:Port"]},password={configuration["Redis:Password"]}")
                .AddRabbitMQ($"amqp://{configuration["RabbitMQOptions:Username"]}:{configuration["RabbitMQOptions:Password"]}@{configuration["RabbitMQOptions:Host"]}:{configuration["RabbitMQOptions:Port"]}", null, null, null, null)
                .AddElasticsearch(configuration["Elastic:Url"]!);

        }
    }
}
