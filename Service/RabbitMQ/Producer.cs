using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Indd.Service.RabbitMQ
{
    class Producer
    {

        public Producer()
        {
            var factory = new ConnectionFactory() { HostName = "172.20.0.5" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "JobIn1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "JobIn1",
                                     basicProperties: null,
                                     body: body);
                
            }

            


    }
}
}
