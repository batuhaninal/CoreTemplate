using RabbitMQ.Client;

namespace Application.Abstractions.Commons.MessageBrokers.Publishers
{
    public interface IRabbitMQPublisherService
    {
        void Publish(string queueName, string exchangeName, byte[] message, string exchangeType = ExchangeType.Direct);
        void Publish<T>(string queueName, string exchangeName, T message, string exchangeType = ExchangeType.Direct);
        void Publish(string queueName, string exchangeName, string message, string exchangeType = ExchangeType.Direct);
    }
}
