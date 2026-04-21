using CollaboratorService.Application.Interfaces;
using CollaboratorService.Domain.Entites;
using CollaboratorService.Infrastructure.Persistence;
using Dapper;

namespace CollaboratorService.Infrastructure.Repositories
{
    // CHANGE: added namespace and clean repository implementation
        public class CollaboratorRepository : ICollaboratorRepository
        {
            private readonly DbConnectionFactory _factory;

            public CollaboratorRepository(DbConnectionFactory factory)
            {
                _factory = factory;
            }

            public async Task<int> AddAsync(Collaborator collaborator)
            {
                const string query = @"
                INSERT INTO Collaborators
                    (NoteId, OwnerUserId, CollaboratorUserId, CreatedAt)
                OUTPUT INSERTED.Id
                VALUES
                    (@NoteId, @OwnerUserId, @CollaboratorUserId, @CreatedAt);
            ";

                using var connection = _factory.CreateConnection();
                return await connection.ExecuteScalarAsync<int>(query, collaborator);
            }

            public async Task<List<Collaborator>> GetAllAsync()
            {
                const string query = @"
                SELECT Id, NoteId, OwnerUserId, CollaboratorUserId, CreatedAt
                FROM Collaborators
                ORDER BY CreatedAt DESC;
            ";

                using var connection = _factory.CreateConnection();
                var result = await connection.QueryAsync<Collaborator>(query);
                return result.ToList();
            }

            public async Task<Collaborator?> GetByIdAsync(int id)
            {
                const string query = @"
                SELECT Id, NoteId, OwnerUserId, CollaboratorUserId, CreatedAt
                FROM Collaborators
                WHERE Id = @Id;
            ";

                using var connection = _factory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<Collaborator>(query, new { Id = id });
            }

            public async Task<bool> UpdateAsync(Collaborator collaborator)
            {
                const string query = @"
                UPDATE Collaborators
                SET NoteId = @NoteId,
                    CollaboratorUserId = @CollaboratorUserId
                WHERE Id = @Id;
            ";

                using var connection = _factory.CreateConnection();
                var rows = await connection.ExecuteAsync(query, collaborator);
                return rows > 0;
            }

            public async Task<bool> RemoveAsync(int id)
            {
                const string query = @"
                DELETE FROM Collaborators
                WHERE Id = @Id;
            ";

                using var connection = _factory.CreateConnection();
                var rows = await connection.ExecuteAsync(query, new { Id = id });
                return rows > 0;
            }
        }
    }
