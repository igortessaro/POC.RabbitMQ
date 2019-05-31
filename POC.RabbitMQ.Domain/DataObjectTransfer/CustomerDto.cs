using System;
using System.Collections.Generic;
using System.Text;

namespace POC.RabbitMQ.Domain.DataObjectTransfer
{
    public class CustomerDto : IDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
