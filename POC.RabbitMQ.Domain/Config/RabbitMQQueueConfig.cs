namespace POC.RabbitMQ.Domain.Config
{
    public class RabbitMQQueueConfig
    {
        public string Name { get; set; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; } = false;

        public bool AutoDelete { get; set; } = false;
    }
}
