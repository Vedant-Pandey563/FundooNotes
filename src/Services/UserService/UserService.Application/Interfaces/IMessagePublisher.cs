using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Interfaces
{
    // Abstraction so we can swap RabbitMQ later if needed
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(
            T message,
            string queueName,
            CancellationToken cancellationToken = default);
    }
}
