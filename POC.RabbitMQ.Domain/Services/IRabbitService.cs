using POC.RabbitMQ.Domain.Config;

namespace POC.RabbitMQ.Domain.Services
{
    public interface IRabbitService : IService
    {
        bool SendMessage<T>(RabbitMQConnectionConfig connectionConfig, RabbitQueueConfig queue, string exchange, string routingKey, T body);
    }
}
