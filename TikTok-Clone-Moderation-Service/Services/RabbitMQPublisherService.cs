using RabbitMQ.Client;
using System.Text;

namespace TikTok_Clone_Moderation_Service.Services
{

    public interface IRabbitMQPublisherService
    {
      public void sendVideoMessage(string message);
            
    }
    public class RabbitMQPublisherService : IRabbitMQPublisherService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;

        public RabbitMQPublisherService()
        {
            //establish connection with rabbitmq
            _connectionFactory = new ConnectionFactory
            {
                HostName = "tiktok-clone-moderation-service-RabbitMQService-1", // Replace with your RabbitMQ Docker container IP address
                Port = 5672,              // RabbitMQ default port
                UserName = "guest",
                Password = "guest"
            };
            _queueName = "ModerationPublishQueue";
            
        }


        public void sendVideoMessage(string message)
        {
            if (message == null) { Console.WriteLine("the conetent of the message is missing ore empty"); }
            else
            {
                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);


                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: properties,
                                     body: body);

                Console.WriteLine($" [x] Sent {message}");

            }
            

        }
    }
}
