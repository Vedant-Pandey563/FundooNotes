using Microsoft.Data.SqlClient;
using System.Data;

namespace CollaboratorService.Infrastructure.Persistence
{
    // CHANGE: implemented SQL connection factory
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}