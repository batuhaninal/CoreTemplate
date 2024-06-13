using RabbitMQ.Client;

namespace Application.Abstractions.Commons.MessageBrokers
{
    public interface IRabbitMQService
    {
        IConnection GetRabbitMQConnection();
    }
}
