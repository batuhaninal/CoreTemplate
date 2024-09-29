using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events.Categories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers
{
    public class CategoryCreatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CategoryCreatedEventConsumer> _logger;
        private readonly IRabbitMQService _rabbitmqService;
        private IModel _channel;
        private IConnection _connection;

        public CategoryCreatedEventConsumer(IServiceProvider serviceProvider, ILogger<CategoryCreatedEventConsumer> logger, IRabbitMQService rabbitmqService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _rabbitmqService = rabbitmqService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.CreateCategoryElastic, true, false, false);
            _channel.QueueBind(QueueNames.CreateCategoryElastic, ExchangeNames.Elastic, QueueNames.CreateCategoryElastic);
            _channel.BasicQos(0, 1, false);


            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Create_Category;

            _channel.BasicConsume(QueueNames.CreateCategoryElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Create_Category(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    byte[] body = @event.Body.ToArray();

                    CategoryCreatedEvent categoryCreatedEvent = JsonSerializer.Deserialize<CategoryCreatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.CreateAsync(categoryCreatedEvent.IndexName, JsonSerializer.Deserialize<Category>(categoryCreatedEvent.Model));

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoryCreatedEventConsumer)} service error : {ex.Message}");
                // dead-letter-queue DLQ eklenmeli
                _channel.BasicNack(@event.DeliveryTag, false, false);
            }
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
