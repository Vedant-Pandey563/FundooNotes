using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Events
{
    // This event represents a successful user registration.
    // It will be published to RabbitMQ.
    public class UserRegisteredEvent
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
