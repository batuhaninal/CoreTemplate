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

namespace Adapter.Services.MessageBrokers.Consumers
{
    public class UserUpdatedEventConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQService _rabbitmqService;
        private IModel _model;
        private IConnection _connection;
        private readonly ILogger<UserUpdatedEventConsumer> _logger;

        public UserUpdatedEventConsumer(IServiceProvider serviceProvider, IRabbitMQService rabbitmqService, ILogger<UserUpdatedEventConsumer> logger)
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
            _model.QueueDeclare(QueueNames.UpdateUserElastic, true, false, false);
            _model.QueueBind(QueueNames.UpdateUserElastic, ExchangeNames.Elastic, QueueNames.UpdateUserElastic);

            _model.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);

            consumer.Received += Update_User;

            _model.BasicConsume(QueueNames.UpdateUserElastic, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Update_User(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var elasticService = scope.ServiceProvider.GetRequiredService<IElasticSearchWriteRepository>();

                byte[] body = @event.Body.ToArray();

                var userUpdatedEvent = JsonSerializer.Deserialize<UserUpdatedEvent>(body)!;

                await elasticService.UpdateAsync(userUpdatedEvent.IndexName, userUpdatedEvent.UserId, JsonSerializer.Deserialize<SecuredUserDto>(userUpdatedEvent.Model));

                _model.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserUpdatedEventConsumer)} service error: {ex.Message}");
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
