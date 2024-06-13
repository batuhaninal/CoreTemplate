namespace Application.Abstractions.Commons.MessageBrokers.Consumers
{
    public interface IConsumerService
    {
        Task ConsumeAsync(string queueName, string exchangeName);
    }
}
