using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    // Contract between Application and Infrastructure
    // Infrastructure will implement this (MongoDB logic)
    public interface IUserRepository
    {
        // Create new user
        Task CreateAsync(User user);

        // Find user by email (used in login)
        Task<User?> GetByEmailAsync(string email);
    }
}
