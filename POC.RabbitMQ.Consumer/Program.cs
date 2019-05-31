using Newtonsoft.Json;
using POC.RabbitMQ.Domain.Command.Customer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace POC.RabbitMQ.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "customer.success",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: "customer.success",
                     autoAck: true,
                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens para processamento");
                Console.WriteLine("Pressione uma tecla para encerrar...");
                Console.ReadKey();
            }
        }

        private static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body);

            CreateCustomerCommand customer = JsonConvert.DeserializeObject<CreateCustomerCommand>(message);

            Console.WriteLine(Environment.NewLine + "[Nova mensagem recebida] " + message);
        }
    }
}
