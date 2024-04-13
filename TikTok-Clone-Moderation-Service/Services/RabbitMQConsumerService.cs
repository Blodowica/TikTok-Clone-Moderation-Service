using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok_Clone_Moderation_Service.Services
{
    public interface IRabbitMQConsumerService
    {
       
        List<string> ReadAllVideoServiceMessages(string queuName);
        List<string> ReadAllUserServiceMessages(string queuName);
    }

    public class RabbitMQConsumerService : IRabbitMQConsumerService
    {
        private readonly ConnectionFactory _connectionFactory;
     

        public RabbitMQConsumerService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "tiktok-clone-moderation-service-RabbitMQService-1", // Replace with your RabbitMQ Docker container IP address
                Port = 5672,              // RabbitMQ default port
                UserName = "guest",
                Password = "guest"
            };

           
        }

        public List<string> ReadAllVideoServiceMessages(string queuName)
        {
            return ReadAllMessagesFromQueue(queuName);
        }

        public List<string> ReadAllUserServiceMessages(string queueName)
        {
            return ReadAllMessagesFromQueue(queueName);
        }

        private List<string> ReadAllMessagesFromQueue(string queueName)
        {
            var messages = new List<string>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    messages.Add(message);
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
                System.Threading.Thread.Sleep(1000);
            }

            return messages;
        }
    }
}
    