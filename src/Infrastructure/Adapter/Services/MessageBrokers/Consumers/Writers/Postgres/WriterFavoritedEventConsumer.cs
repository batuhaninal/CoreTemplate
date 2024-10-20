using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Services.Writers;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events.Writers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers.Writers.Postgres
{
    public class WriterFavoritedEventConsumer : BackgroundService
    {
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WriterFavoritedEventConsumer> _logger;
        private IModel _channel;
        private IConnection _connection;

        public WriterFavoritedEventConsumer(IRabbitMQService rabbitmqService, IServiceProvider serviceProvider, ILogger<WriterFavoritedEventConsumer> logger)
        {
            _rabbitmqService = rabbitmqService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeNames.Writer, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(QueueNames.WriterFavorite, true, false, false);
            _channel.QueueBind(QueueNames.WriterFavorite, ExchangeNames.Writer, QueueNames.WriterFavorite);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Favorite_Writer;

            _channel.BasicConsume(QueueNames.WriterFavorite, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Favorite_Writer(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var body = @event.Body.ToArray();
                    WriterFavoritedEvent writerFavoritedEvent = JsonSerializer.Deserialize<WriterFavoritedEvent>(body)!;

                    if(writerFavoritedEvent is null)
                        throw new ArgumentNullException(nameof(writerFavoritedEvent));

                    IWriterService writerService = scope.ServiceProvider.GetRequiredService<IWriterService>();

                    await writerService.CreateFavAsync(writerFavoritedEvent.WriterId, writerFavoritedEvent.UserId);

                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(WriterFavoritedEventConsumer)} background service unexpected error: {ex.Message}");
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
