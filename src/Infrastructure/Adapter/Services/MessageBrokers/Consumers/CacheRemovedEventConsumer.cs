using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers
{
    public class CacheRemovedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly ICacheService _cacheService;
        private IModel _channel;
        private IConnection _connection;
        private readonly ILogger<CacheRemovedEventConsumer> _logger;
        private readonly IOutputCacheStore _outputCacheStore;

        public CacheRemovedEventConsumer(IRabbitMQService rabbitmqService, ICacheService cacheService, ILogger<CacheRemovedEventConsumer> logger, IOutputCacheStore outputCacheStore)
        {
            _rabbitmqService = rabbitmqService;
            _cacheService = cacheService;
            _logger = logger;
            _outputCacheStore = outputCacheStore;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Cache, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.CacheRemove, true, false, false);
            _channel.QueueBind(QueueNames.CacheRemove, ExchangeNames.Cache, QueueNames.CacheRemove);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, @event) =>
            {
                try
                {
                    var body = @event.Body.ToArray();
                    var cacheRemovedEvent = JsonSerializer.Deserialize<CacheRemovedEvent>(body);

                    if (cacheRemovedEvent != null)
                    {
                        if (cacheRemovedEvent.OutputCacheTags is not null && cacheRemovedEvent.OutputCacheTags.Any())
                        {
                            foreach (string octag in cacheRemovedEvent.OutputCacheTags)
                            {
                                _logger.LogInformation($"{octag}");
                                await _outputCacheStore.EvictByTagAsync(octag, stoppingToken);
                            }
                        }

                        if (cacheRemovedEvent.CachePrefixes is not null && cacheRemovedEvent.CachePrefixes.Any())
                        {
                            foreach (string cachePrefix in cacheRemovedEvent.CachePrefixes)
                            {
                                _logger.LogInformation($"{cachePrefix}");
                                await _cacheService.DeleteAllWithPrefixAsync(cachePrefix);
                            }
                        }
                    }

                    _channel.BasicAck(@event.DeliveryTag, false);   
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Service: {nameof(CacheRemovedEventConsumer)}, Error: {ex.Message}, InnerEx: {ex.InnerException}");
                    // dead-letter-queue DLQ eklenmeli
                    _channel.BasicNack(@event.DeliveryTag, false, false);
                }
            };

            _channel.BasicConsume(QueueNames.CacheRemove, false, consumer);

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _connection?.Close();

            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }

}
