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

namespace Adapter.Services.MessageBrokers.Consumers.Writers.ELKs
{
    public class WriterUpdatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WriterUpdatedEventConsumer> _logger;
        private readonly IRabbitMQService _rabbitmqService;
        private IModel _model;
        private IConnection _connection;

        public WriterUpdatedEventConsumer(IServiceProvider serviceProvider, ILogger<WriterUpdatedEventConsumer> logger, IRabbitMQService rabbitmqService)
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
            _model.QueueDeclare(QueueNames.UpdateWriterElastic, true, false, false);
            _model.QueueBind(QueueNames.UpdateWriterElastic, ExchangeNames.Elastic, QueueNames.UpdateWriterElastic);

            _model.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += Update_Writer;

            _model.BasicConsume(QueueNames.UpdateWriterElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Update_Writer(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    byte[] body = @event.Body.ToArray();

                    var writerUpdatedEvent = JsonSerializer.Deserialize<WriterUpdatedEvent>(body)!;

                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                    await elasticService.UpdateAsync(writerUpdatedEvent.IndexName, writerUpdatedEvent.WriterId, JsonSerializer.Deserialize<Writer>(writerUpdatedEvent.Model));

                    _model.BasicAck(@event.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(WriterUpdatedEventConsumer)} service error: {ex.Message}");
                // dead-letter-queue DLQ eklenmeli
                _model.BasicNack(@event.DeliveryTag, false, false);
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
