using System;

namespace POC.RabbitMQ.Domain.Command.Customer
{
    [Serializable]
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
