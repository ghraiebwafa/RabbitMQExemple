using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace rabbitMQ.Services
{
    public class RabbitMqService
    {
        private readonly string _hostName = "rabbitmq";
        private readonly string _queueName = "itemQueue";

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

        public void ListenForMessages()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($" [x] Received {message}");
            };
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }
    }
}