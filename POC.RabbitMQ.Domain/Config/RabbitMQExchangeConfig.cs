using System;
using System.Collections.Generic;
using System.Text;

namespace POC.RabbitMQ.Domain.Config
{
    public class RabbitMQExchangeConfig
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool Durable { get; set; } = false;

        public bool AutoDelete { get; set; } = false;
    }
}
