using POC.RabbitMQ.Domain.Command.Customer;
using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Domain.Services;

namespace POC.RabbitMQ.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        public RabbitMQConfig RabbitMQConfig { get; set; }

        public IRabbitService RabbitService { get; set; }

        public CustomerService(RabbitMQConfig rabbitMQConfig, IRabbitService rabbitService)
        {
            this.RabbitMQConfig = rabbitMQConfig;
            this.RabbitService = rabbitService;
        }

        public bool ReceiveCustomer(CreateCustomerCommand customer)
        {
            return this.RabbitService.SendMessage(
                this.RabbitMQConfig.CustomerRabbitMQ.Connection,
                this.RabbitMQConfig.CustomerRabbitMQ.Queue,
                this.RabbitMQConfig.CustomerRabbitMQ.Exchange,
                this.RabbitMQConfig.CustomerRabbitMQ.RoutingKey,
                customer);
        }
    }
}
