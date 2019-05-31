using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Domain.Services;
using RabbitMQ.Client;
using System;
using System.Text;

namespace POC.RabbitMQ.Infrastructure.Facade
{
    public class RabbitService : IRabbitService
    {
        public bool SendMessage<T>(RabbitMQConnectionConfig connectionConfig, RabbitQueueConfig queue, string exchange, string routingKey, T body)
        {
            if (connectionConfig == null)
            {
                throw new ArgumentNullException(nameof(connectionConfig));
            }

            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = connectionConfig.HostName,
                Port = connectionConfig.Port,
                UserName = connectionConfig.UserName,
                Password = connectionConfig.Password
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue.Name,
                                     durable: queue.Durable,
                                     exclusive: queue.Exclusive,
                                     autoDelete: queue.AutoDelete,
                                     arguments: null);

                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = true;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                byte[] payload = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: payload);
            }

            return true;
        }
    }
}
