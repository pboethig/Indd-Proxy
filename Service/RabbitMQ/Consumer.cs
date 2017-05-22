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

            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "JobIn1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                while (true)
                {

                    BasicGetResult result = channel.BasicGet("JobIn1", true);

                    if (result == null) continue;

                    var message = Encoding.UTF8.GetString(result.Body);

                    try
                    {
                        Factory commandFactory = new Factory();

                        dynamic ticket = commandFactory.prepare(message);

                        CommandResponse response = commandFactory.processTicket(ticket);

                        if (response.errors.Count > 0)
                        {

                        }
                        else
                        {

                        }
                    }
                    catch (System.Exception ex)
                    {
                        Indd.Service.Log.Syslog.log("Consumer error: " + ex.Message + message);
                    }
                }
            }
            
        }
        
     }
}
