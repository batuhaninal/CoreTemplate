using Application.Abstractions.Commons.MessageBrokers;
using Application.Models.Constants.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Adapter.Services.MessageBrokers
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQOptions _rabbitmqOptions;
        public RabbitMQService(IOptionsMonitor<RabbitMQOptions> options)
        {
            _rabbitmqOptions = options.CurrentValue;
        }
        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _rabbitmqOptions.Host,
                Port = _rabbitmqOptions.Port,
                UserName = _rabbitmqOptions.UserName,
                Password = _rabbitmqOptions.Password,
                DispatchConsumersAsync = true
            };

            return factory.CreateConnection();
        }
    }
}
