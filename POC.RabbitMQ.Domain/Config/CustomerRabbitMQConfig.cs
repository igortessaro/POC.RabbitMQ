namespace POC.RabbitMQ.Domain.Config
{
    public class CustomerRabbitMQConfig
    {
        public RabbitMQConnectionConfig Connection { get; set; }

        public RabbitQueueConfig Queue { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }
    }
}