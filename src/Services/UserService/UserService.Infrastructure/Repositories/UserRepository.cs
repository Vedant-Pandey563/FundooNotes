using Dapper;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories
{
    // Infrastructure implementation of the user repository.
    // This class owns all SQL access for the UserService.
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _factory;

        public UserRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        // Insert a new user into the Users table.
        public async Task CreateAsync(User user)
        {
            const string query = @"
                INSERT INTO Users (FirstName, LastName, Email, PasswordHash)
                VALUES (@FirstName, @LastName, @Email, @PasswordHash);";

            using var connection = _factory.CreateConnection();
            await connection.ExecuteAsync(query, user);
        }

        // Fetch a single user by email.
        public async Task<User?> GetByEmailAsync(string email)
        {
            const string query = @"
                SELECT TOP 1 *
                FROM Users
                WHERE Email = @Email;";

            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}