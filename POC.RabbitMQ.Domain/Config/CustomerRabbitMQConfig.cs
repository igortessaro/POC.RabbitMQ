namespace POC.RabbitMQ.Domain.Config
{
    public class CustomerRabbitMQConfig
    {
        public RabbitMQConnectionConfig Connection { get; set; }

        public RabbitMQQueueConfig Queue { get; set; }

        public RabbitMQExchangeConfig Exchange { get; set; }

        public string RoutingKey { get; set; }
    }
}