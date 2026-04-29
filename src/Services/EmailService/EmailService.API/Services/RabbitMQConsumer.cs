using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService.API.Services
{
    // Background worker that listens for user registration events from RabbitMQ.
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQConsumer()
        {
            // Use IPv4 loopback explicitly to avoid localhost -> ::1 issues on Windows.
            _factory = new ConnectionFactory
            {
                HostName = "127.0.0.1"
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var connection = await _factory.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: "user.registered",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Avoid null reference issues if the payload is malformed.
                var data = JsonSerializer.Deserialize<UserRegisteredEvent>(json) ?? new UserRegisteredEvent();

                Console.WriteLine($"[EMAIL SENT] Welcome {data.FirstName} ({data.Email})");

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "user.registered",
                autoAck: true,
                consumer: consumer);

            // Keep the background worker alive until cancellation.
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

    public class UserRegisteredEvent
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}