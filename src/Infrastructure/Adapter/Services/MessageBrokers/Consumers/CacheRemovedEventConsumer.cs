using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events;
using Microsoft.Extensions.Hosting;
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

        public CacheRemovedEventConsumer(IRabbitMQService rabbitmqService, ICacheService cacheService)
        {
            _rabbitmqService = rabbitmqService;
            _cacheService = cacheService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Cache, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.Cache, true, false, false);
            _channel.QueueBind(QueueNames.Cache, ExchangeNames.Cache, QueueNames.Cache);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, @event) =>
            {
                var body = @event.Body.ToArray();
                var cacheRemovedEvent = JsonSerializer.Deserialize<CacheRemovedEvent>(body);

                if (cacheRemovedEvent != null)
                {
                    await _cacheService.DeleteAllWithPrefixAsync(cacheRemovedEvent.CachePrefix);
                }

                _channel.BasicAck(@event.DeliveryTag, false);
            };

            _channel.BasicConsume(QueueNames.Cache, false, consumer);

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
