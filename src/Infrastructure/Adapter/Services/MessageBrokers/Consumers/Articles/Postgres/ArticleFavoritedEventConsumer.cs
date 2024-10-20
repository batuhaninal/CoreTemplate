using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Services.Articles;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events.Articles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers.Articles.Postgres
{
    public class ArticleFavoritedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IServiceProvider _serviceProvider;
        private ILogger<ArticleFavoritedEventConsumer> _logger;
        private IModel _channel;
        private IConnection _connection;

        public ArticleFavoritedEventConsumer(IRabbitMQService rabbitmqService, IServiceProvider serviceProvider, ILogger<ArticleFavoritedEventConsumer> logger)
        {
            _rabbitmqService = rabbitmqService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Article, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.ArticleFavorite, true, false, false);
            _channel.QueueBind(QueueNames.ArticleFavorite, ExchangeNames.Article, QueueNames.ArticleFavorite);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Favorite_Article;

            _channel.BasicConsume(QueueNames.ArticleFavorite, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Favorite_Article(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = @event.Body.ToArray();
                    ArticleFavoritedEvent? favoritedArticle = JsonSerializer.Deserialize<ArticleFavoritedEvent>(body);
                    if (favoritedArticle is null)
                        throw new ArgumentNullException(nameof(favoritedArticle));

                    IArticleService articleService = scope.ServiceProvider.GetRequiredService<IArticleService>();

                    await articleService.CreateFavAsync(favoritedArticle.ArticleId, favoritedArticle.UserId);

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ArticleFavoritedEventConsumer)} background service unexpected error: {ex.Message}");
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
