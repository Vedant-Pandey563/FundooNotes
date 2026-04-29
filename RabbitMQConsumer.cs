using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService.API.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQConsumer()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "user.registered",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var data = JsonSerializer.Deserialize<UserRegisteredEvent>(json);

                // 🔥 Simulate sending email
                Console.WriteLine($"[EMAIL SENT] Welcome {data?.FirstName} ({data?.Email})");
            };

            channel.BasicConsume(
                queue: "user.registered",
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }

    public class UserRegisteredEvent
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
    }
}