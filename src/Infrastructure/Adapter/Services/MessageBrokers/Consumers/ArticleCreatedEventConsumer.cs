using Adapter.Services.Caching;
using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events;
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
    public class ArticleCreatedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        private IConnection _connection;
        private ILogger<ArticleCreatedEventConsumer> _logger;
        public ArticleCreatedEventConsumer(IRabbitMQService rabbitmqService, IServiceProvider serviceProvider, ILogger<ArticleCreatedEventConsumer> logger)
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
            _channel.QueueDeclare(QueueNames.CreateArticleElastic, true, false, false);
            _channel.QueueBind(QueueNames.CreateArticleElastic, ExchangeNames.Elastic, QueueNames.CreateArticleElastic);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += CreateArticle;

            _channel.BasicConsume(QueueNames.CreateArticleElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task CreateArticle(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = @event.Body.ToArray();
                    var articleCreatedEvent = JsonSerializer.Deserialize<ArticleCreatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.CreateAsync(articleCreatedEvent.IndexName, JsonSerializer.Deserialize<Article>(articleCreatedEvent.Model));

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ArticleCreatedEventConsumer error: "+ex.Message);
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
