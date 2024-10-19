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

namespace Adapter.Services.MessageBrokers.Consumers.Categories.ELKs
{
    public class CategoryUpdatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        private IConnection _connection;
        private readonly ILogger<CategoryUpdatedEventConsumer> _logger;
        private readonly IRabbitMQService _rabbitMQService;

        public CategoryUpdatedEventConsumer(IServiceProvider serviceProvider, ILogger<CategoryUpdatedEventConsumer> logger, IRabbitMQService rabbitMQService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _rabbitMQService = rabbitMQService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitMQService.GetRabbitMQConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.UpdateCategoryElastic, true, false, false);
            _channel.QueueBind(QueueNames.UpdateCategoryElastic, ExchangeNames.Elastic, QueueNames.UpdateCategoryElastic);

            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Update_Category;

            _channel.BasicConsume(QueueNames.UpdateCategoryElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Update_Category(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    byte[] body = @event.Body.ToArray();

                    var categoryUpdatedEvent = JsonSerializer.Deserialize<CategoryUpdatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.UpdateAsync(categoryUpdatedEvent.IndexName, categoryUpdatedEvent.CategoryId, JsonSerializer.Deserialize<Category>(categoryUpdatedEvent.Model));

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoryUpdatedEventConsumer)} service error: {ex.Message}");
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
