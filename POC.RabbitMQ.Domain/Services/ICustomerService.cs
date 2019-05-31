using POC.RabbitMQ.Domain.Command.Customer;

namespace POC.RabbitMQ.Domain.Services
{
    public interface ICustomerService : IService
    {
        bool ReceiveCustomer(CreateCustomerCommand customer);
    }
}
