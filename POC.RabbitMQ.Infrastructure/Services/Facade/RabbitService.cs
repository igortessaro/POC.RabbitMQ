using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Domain.DataObjectTransfer;
using POC.RabbitMQ.Domain.Services;
using RabbitMQ.Client;
using System;
using System.Text;

namespace POC.RabbitMQ.Infrastructure.Services
{
    public class RabbitService : IRabbitService
    {
        public QueueStatusDto GetQueueStatus(RabbitMQConnectionConfig connectionConfig, RabbitMQQueueConfig queue, RabbitMQExchangeConfig exchange, string routingKey)
        {
            this.ValidateRules(connectionConfig, queue, exchange);

            QueueStatusDto result;

            ConnectionFactory factory = this.CreateConnectionFactory(connectionConfig);

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                QueueDeclareOk queueDeclare = channel.QueueDeclarePassive(queue.Name);
                result = new QueueStatusDto(queueDeclare.QueueName, (int)queueDeclare.MessageCount, (int)queueDeclare.ConsumerCount);
            }

            return result;
        }

        public bool SendMessage<T>(RabbitMQConnectionConfig connectionConfig, RabbitMQQueueConfig queue, RabbitMQExchangeConfig exchange, string routingKey, T body)
        {
            this.ValidateRules(connectionConfig, queue, exchange);

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            ConnectionFactory factory = this.CreateConnectionFactory(connectionConfig);

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                // Criar exchange
                channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);

                // Criar fila...
                channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete);

                // Bind da fila com a exchange/routingkey
                channel.QueueBind(queue.Name, exchange.Name, routingKey, null);

                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = true;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
                byte[] payload = Encoding.UTF8.GetBytes(json);

                // Send
                channel.BasicPublish(exchange.Name, routingKey, basicProperties, payload);
            }

            return true;
        }

        private void ValidateRules(RabbitMQConnectionConfig connectionConfig, RabbitMQQueueConfig queue, RabbitMQExchangeConfig exchange)
        {
            if (connectionConfig == null)
            {
                throw new ArgumentNullException(nameof(connectionConfig));
            }

            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (exchange == null)
            {
                throw new ArgumentNullException(nameof(exchange));
            }
        }

        private ConnectionFactory CreateConnectionFactory(RabbitMQConnectionConfig connectionConfig)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = connectionConfig.HostName,
                Port = connectionConfig.Port,
                UserName = connectionConfig.UserName,
                Password = connectionConfig.Password
            };

            return factory;
        }
    }
}
