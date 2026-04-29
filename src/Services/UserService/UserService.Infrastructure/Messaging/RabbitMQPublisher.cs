using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Messaging
{
    // Publishes events to RabbitMQ after successful business operations.
    public class RabbitMQPublisher : IMessagePublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQPublisher()
        {
            // Use IPv4 loopback explicitly to avoid Windows resolving localhost to ::1.
            _factory = new ConnectionFactory
            {
                HostName = "127.0.0.1"
            };
        }

        public async Task PublishAsync<T>(
            T message,
            string queueName,
            CancellationToken cancellationToken = default)
        {
            // Open a RabbitMQ connection and channel using the async v7 API.
            await using var connection = await _factory.CreateConnectionAsync(cancellationToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            // Declare the queue so the publish works even if the queue does not exist yet.
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            // Serialize the payload as JSON.
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            // RabbitMQ.Client v7 requires a concrete header type for publish properties.
            var props = new BasicProperties();

            await channel.BasicPublishAsync<BasicProperties>(
                exchange: string.Empty,
                routingKey: queueName,
                mandatory: false,
                basicProperties: props,
                body: body,
                cancellationToken: cancellationToken);
        }
    }
}