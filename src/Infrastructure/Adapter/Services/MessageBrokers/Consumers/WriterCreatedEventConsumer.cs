using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using Application.Models.MessageBrokers.Events.Writers;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers
{
    public class WriterCreatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WriterCreatedEventConsumer> _logger;
        private readonly IRabbitMQService _rabbitmqService;
        private IModel _model;
        private IConnection _connection;

        public WriterCreatedEventConsumer(IServiceProvider serviceProvider, ILogger<WriterCreatedEventConsumer> logger, IRabbitMQService rabbitmqService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _rabbitmqService = rabbitmqService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();

            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _model.QueueDeclare(QueueNames.CreateWriterElastic, true, false, false);
            _model.QueueBind(QueueNames.CreateWriterElastic, ExchangeNames.Elastic, QueueNames.CreateWriterElastic);

            _model.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += Create_Writer;

            _model.BasicConsume(QueueNames.CreateWriterElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Create_Writer(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    byte[] body = @event.Body.ToArray();

                    var writerCreatedEvent = JsonSerializer.Deserialize<WriterCreatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.CreateAsync(writerCreatedEvent.IndexName, JsonSerializer.Deserialize<Writer>(writerCreatedEvent.Model));

                    _model.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(WriterCreatedEventConsumer)} service error: {ex.Message}");
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
