using POC.RabbitMQ.Domain.Command.Customer;
using POC.RabbitMQ.Domain.DataObjectTransfer;

namespace POC.RabbitMQ.Domain.Services
{
    public interface ICustomerService : IService
    {
        bool ReceiveCustomer(CreateCustomerCommand customer);

        QueueStatusDto GetQueueStatus();
    }
}
