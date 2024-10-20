using Application.Abstractions.Commons.MessageBrokers;
using Application.Models.Constants.MessageBrokers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

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
            throw new NotImplementedException();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
