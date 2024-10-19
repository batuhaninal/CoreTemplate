using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Repositories.Commons;
using Application.Models.Constants.MessageBrokers;
using Application.Models.DTOs.Users;
using Application.Models.MessageBrokers.Events.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Adapter.Services.MessageBrokers.Consumers.Users.ELKs
{
    public class UserCreatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQService _rabbitmqService;
        private IModel _model;
        private IConnection _connection;
        private readonly ILogger<UserCreatedEventConsumer> _logger;

        public UserCreatedEventConsumer(IServiceProvider serviceProvider, IRabbitMQService rabbitmqService, ILogger<UserCreatedEventConsumer> logger)
        {
            _serviceProvider = serviceProvider;
            _rabbitmqService = rabbitmqService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _rabbitmqService.GetRabbitMQConnection();

            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeNames.Elastic, ExchangeType.Direct, true, false);
            _model.QueueDeclare(QueueNames.CreateUserElastic, true, false, false);
            _model.QueueBind(QueueNames.CreateUserElastic, ExchangeNames.Elastic, QueueNames.CreateUserElastic);

            _model.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += Create_User;

            _model.BasicConsume(QueueNames.CreateUserElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Create_User(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                byte[] body = @event.Body.ToArray();

                var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(body)!;

                await elasticService.CreateAsync(userCreatedEvent.IndexName, JsonSerializer.Deserialize<SecuredUserDto>(userCreatedEvent.Model));

                _model.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserCreatedEventConsumer)} service error: {ex.Message}");
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
