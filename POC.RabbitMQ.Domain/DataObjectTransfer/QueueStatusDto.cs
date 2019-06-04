namespace POC.RabbitMQ.Domain.DataObjectTransfer
{
    public class QueueStatusDto : IDto
    {
        public QueueStatusDto()
        {
        }

        public QueueStatusDto(string name, int messageCount, int consumerCount)
            : this()
        {
            this.Name = name;
            this.MessageCount = messageCount;
            this.ConsumerCount = consumerCount;
        }

        public string Name { get; set; }

        public int MessageCount { get; set; }

        public int ConsumerCount { get; set; }
    }
}
