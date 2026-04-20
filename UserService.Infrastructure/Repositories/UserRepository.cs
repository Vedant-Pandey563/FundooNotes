using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MongoDB.Driver;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories
{
    //Implement IuserRepo from Application layer
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _factory;

        public UserRepository(DbConnectionFactory factory)
        {
            _factory=factory;
        }

        //create new user
        public async Task CreateAsync(User user)
        {
            var query = @"
                        Insert Into Users(FirstName,LastName,Email,PasswordHash)
                        Values(@FirstName,@LastName,@Email,@PasswordHash)";

            using var connection = _factory.CreateConnection();

            await connection.ExecuteAsync(query, user);
        }
        //find user by email
        public async Task<User?> GetByEmailAsync(string email)
        {
            var query = @"
                        Select * from Users Where Email = @Email";

            using var connection = _factory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
