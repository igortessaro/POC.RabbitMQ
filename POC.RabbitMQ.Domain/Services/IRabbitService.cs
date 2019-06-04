using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Domain.DataObjectTransfer;

namespace POC.RabbitMQ.Domain.Services
{
    public interface IRabbitService : IService
    {
        bool SendMessage<T>(RabbitMQConnectionConfig connectionConfig, RabbitMQQueueConfig queue, RabbitMQExchangeConfig exchange, string routingKey, T body);

        QueueStatusDto GetQueueStatus(RabbitMQConnectionConfig connectionConfig, RabbitMQQueueConfig queue, RabbitMQExchangeConfig exchange, string routingKey);
    }
}
