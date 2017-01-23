using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace _9.TopicExchange
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs",
                                        type: "topic");
                var routerKey = "anonymous.info";

                var message = string.Empty;
                Console.WriteLine("enter your message");

                while (message != "exit")
                {
                    message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "topic_logs",
                                         routingKey: routerKey,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routerKey, message);
                }
            }
        }
    }
}
