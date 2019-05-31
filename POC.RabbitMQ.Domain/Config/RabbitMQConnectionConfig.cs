namespace POC.RabbitMQ.Domain.Config
{
    public class RabbitMQConnectionConfig
    {
        public string HostName { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
