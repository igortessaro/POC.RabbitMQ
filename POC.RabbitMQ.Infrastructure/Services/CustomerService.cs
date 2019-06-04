using POC.RabbitMQ.Domain.Command.Customer;
using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Domain.DataObjectTransfer;
using POC.RabbitMQ.Domain.Services;

namespace POC.RabbitMQ.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerRabbitMQConfig CustomerRabbitMQConfig { get; set; }

        public IRabbitService RabbitService { get; set; }

        public CustomerService(CustomerRabbitMQConfig customerRabbitMQConfig, IRabbitService rabbitService)
        {
            this.CustomerRabbitMQConfig = customerRabbitMQConfig;
            this.RabbitService = rabbitService;
        }

        public QueueStatusDto GetQueueStatus()
        {
            return this.RabbitService.GetQueueStatus(
                this.CustomerRabbitMQConfig.Connection,
                this.CustomerRabbitMQConfig.Queue,
                this.CustomerRabbitMQConfig.Exchange,
                this.CustomerRabbitMQConfig.RoutingKey);
        }

        public bool ReceiveCustomer(CreateCustomerCommand customer)
        {
            return this.RabbitService.SendMessage(
                this.CustomerRabbitMQConfig.Connection,
                this.CustomerRabbitMQConfig.Queue,
                this.CustomerRabbitMQConfig.Exchange,
                this.CustomerRabbitMQConfig.RoutingKey,
                customer);
        }
    }
}
