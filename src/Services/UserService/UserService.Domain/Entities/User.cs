using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domain.Entities
{
    // This is the core business model for User
    // No external dependencies like EF, API, etc.
    // Pure domain entity (Clean Architecture principle)

    public class User
    {
        // SQL int identity 
        public int Id { get; set; }

        // User's first name
        public string FirstName { get; set; } = string.Empty;

        // User's last name
        public string LastName { get; set; } = string.Empty;

        // Email (used for login)
        public string Email { get; set; } = string.Empty;

        // Hashed password (NEVER store plain password)
        public string PasswordHash { get; set; } = string.Empty;

        // Timestamp when user was created
        public DateTime CreatedAt { get; set; } 
    }
}
