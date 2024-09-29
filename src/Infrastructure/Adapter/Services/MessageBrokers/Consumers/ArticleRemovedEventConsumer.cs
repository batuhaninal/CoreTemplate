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
    public class ArticleRemovedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        private IConnection _connection;
        private ILogger<ArticleRemovedEventConsumer> _logger;
        public ArticleRemovedEventConsumer(IRabbitMQService rabbitmqService, IServiceProvider serviceProvider, ILogger<ArticleRemovedEventConsumer> logger)
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
            _channel.QueueDeclare(QueueNames.RemoveArticleElastic, true, false, false);
            _channel.QueueBind(QueueNames.RemoveArticleElastic, ExchangeNames.Elastic, QueueNames.RemoveArticleElastic);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Remove_Article;

            _channel.BasicConsume(QueueNames.RemoveArticleElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Remove_Article(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = @event.Body.ToArray();
                    var articleRemovedEvent = JsonSerializer.Deserialize<ArticleRemovedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.RemoveAsync<Article>(articleRemovedEvent.IndexName, articleRemovedEvent.ArticleId);

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ArticleRemovedEventConsumer error: " + ex.Message);
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
