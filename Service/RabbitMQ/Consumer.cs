using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Indd.Service.Commands;
using CommandResponse = Indd.Service.Commands.Response;


namespace Indd.Service.RabbitMQ
{
    public class Consumer
    {

        public Consumer()
        {
                
             string host = Indd.Service.Config.Manager.getMessageQueueParameter("host");

            var factory = new ConnectionFactory() { HostName = host };

            IConnection connection = factory.CreateConnection();

            IModel channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: "JobIn1",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var consumer = new EventingBasicConsumer(channel);
    
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                processQueue(message);
            };

            channel.BasicConsume(queue: "JobIn1", noAck: true, consumer: consumer);
        }

        void processQueue(string message)
        {
            try
            {
                Factory commandFactory = new Factory();

                dynamic ticket = commandFactory.prepare(message);

                CommandResponse response = commandFactory.processTicket(ticket);

            }
            catch (System.Exception ex)
            {
                Indd.Service.Log.Syslog.log("Consumer error: " + ex.Message + message);
            }
        }
     }
}
