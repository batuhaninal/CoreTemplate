using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events.Articles;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers
{
    public class ArticleUpdatedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        private IConnection _connection;
        private ILogger<ArticleUpdatedEventConsumer> _logger;
        public ArticleUpdatedEventConsumer(IRabbitMQService rabbitmqService, IServiceProvider serviceProvider, ILogger<ArticleUpdatedEventConsumer> logger)
        {
            _rabbitmqService = rabbitmqService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.UpdateArticleElastic, true, false, false);
            _channel.QueueBind(QueueNames.UpdateArticleElastic, ExchangeNames.Elastic, QueueNames.UpdateArticleElastic);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Update_Article;

            _channel.BasicConsume(QueueNames.UpdateArticleElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Update_Article(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = @event.Body.ToArray();
                    var articleUpdatedEvent = JsonSerializer.Deserialize<ArticleUpdatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.UpdateAsync(articleUpdatedEvent.IndexName, articleUpdatedEvent.ArticleId, JsonSerializer.Deserialize<Article>(articleUpdatedEvent.Model));

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ArticleUpdatedEventConsumer error: " + ex.Message);
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
