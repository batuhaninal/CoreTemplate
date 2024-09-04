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
    public class CategoryRemovedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQService _rabbitMQService;
        private IConnection _connection;
        private IModel _model;
        private readonly ILogger<CategoryRemovedEventConsumer> _logger;

        public CategoryRemovedEventConsumer(IServiceProvider serviceProvider, IRabbitMQService rabbitMQService, ILogger<CategoryRemovedEventConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQService = rabbitMQService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitMQService.GetRabbitMQConnection();

            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _model.QueueDeclare(QueueNames.RemoveCategoryElastic, true, false, false);
            _model.QueueBind(QueueNames.RemoveCategoryElastic, ExchangeNames.Elastic, QueueNames.RemoveCategoryElastic);

            _model.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += Remove_Category;

            _model.BasicConsume(QueueNames.RemoveCategoryElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Remove_Category(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    byte[] body = @event.Body.ToArray();

                    var categoryRemovedEvent = JsonSerializer.Deserialize<CategoryRemovedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.DeleteAsync<Category>(categoryRemovedEvent.IndexName, categoryRemovedEvent.CategoryId);

                    _model.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CategoryRemovedEventConsumer)} service error: {ex.Message}");
                throw;
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _model?.Close();
            _connection?.Close();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
