using Application.Abstractions.Commons.MessageBrokers;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Models.Constants.MessageBrokers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Adapter.Services.MessageBrokers.Publishers
{
    public class RabbitMQPublisherService : IRabbitMQPublisherService
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQPublisherService(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public void Publish(string queueName, string exchangeName, byte[] message, string exchangeType = ExchangeType.Direct)
        {
            var connection = _rabbitMQService.GetRabbitMQConnection();

            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.ExchangeDeclare(exchangeName, exchangeType, true, false);
                channel.QueueDeclare(queueName, true, false, false);

                channel.BasicPublish(exchangeName, queueName, true, properties, new ReadOnlyMemory<byte>(message));
            }
        }

        public void Publish<T>(string queueName, string exchangeName, T message, string exchangeType = ExchangeType.Direct) =>
            Publish(queueName, exchangeName, JsonSerializer.Serialize<T>(message), exchangeType);

        public void Publish(string queueName, string exchangeName, string message, string exchangeType = ExchangeType.Direct) =>
            Publish(queueName, exchangeName, Encoding.UTF8.GetBytes(message), exchangeType);
    }
}
