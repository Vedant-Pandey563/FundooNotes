using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LabelService.Application.Interfaces;
using LabelService.Domain.Entities;
using LabelService.Infrastructure.Persistence;

namespace LabelService.Infrastructure.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly DbConnectionFactory _factory;

        public LabelRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> CreateAsync(Label label)
        {
            const string query = @"
                INSERT INTO Labels (Name, OwnerUserId, CreatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Name, @OwnerUserId, @CreatedAt);";

            using var connection = _factory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, label);
        }

        public async Task<List<Label>> GetAllAsync(int ownerUserId)
        {
            const string query = @"
                SELECT Id, Name, OwnerUserId, CreatedAt
                FROM Labels
                WHERE OwnerUserId = @OwnerUserId
                ORDER BY CreatedAt DESC;";

            using var connection = _factory.CreateConnection();
            var result = await connection.QueryAsync<Label>(query, new { OwnerUserId = ownerUserId });
            return result.ToList();
        }

        public async Task<Label?> GetByIdAsync(int id, int ownerUserId)
        {
            const string query = @"
                SELECT Id, Name, OwnerUserId, CreatedAt
                FROM Labels
                WHERE Id = @Id AND OwnerUserId = @OwnerUserId;";

            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Label>(query, new { Id = id, OwnerUserId = ownerUserId });
        }

        public async Task<Label?> GetByNameAsync(string name, int ownerUserId)
        {
            const string query = @"
                SELECT Id, Name, OwnerUserId, CreatedAt
                FROM Labels
                WHERE Name = @Name AND OwnerUserId = @OwnerUserId;";

            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Label>(query, new { Name = name, OwnerUserId = ownerUserId });
        }

        public async Task<bool> UpdateAsync(Label label)
        {
            const string query = @"
                UPDATE Labels
                SET Name = @Name
                WHERE Id = @Id AND OwnerUserId = @OwnerUserId;";

            using var connection = _factory.CreateConnection();
            var rows = await connection.ExecuteAsync(query, label);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id, int ownerUserId)
        {
            const string query = @"
                DELETE FROM Labels
                WHERE Id = @Id AND OwnerUserId = @OwnerUserId;";

            using var connection = _factory.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { Id = id, OwnerUserId = ownerUserId });
            return rows > 0;
        }

        public async Task<bool> AssignToNoteAsync(NoteLabel noteLabel)
        {
            const string existsQuery = @"
                SELECT COUNT(1)
                FROM NoteLabels
                WHERE NoteId = @NoteId AND LabelId = @LabelId AND OwnerUserId = @OwnerUserId;";

            const string insertQuery = @"
                INSERT INTO NoteLabels (NoteId, LabelId, OwnerUserId, CreatedAt)
                VALUES (@NoteId, @LabelId, @OwnerUserId, @CreatedAt);";

            using var connection = _factory.CreateConnection();

            var exists = await connection.ExecuteScalarAsync<int>(existsQuery, noteLabel);
            if (exists > 0) return false;

            var rows = await connection.ExecuteAsync(insertQuery, noteLabel);
            return rows > 0;
        }

        public async Task<bool> RemoveFromNoteAsync(string noteId, int labelId, int ownerUserId)
        {
            const string query = @"
                DELETE FROM NoteLabels
                WHERE NoteId = @NoteId AND LabelId = @LabelId AND OwnerUserId = @OwnerUserId;";

            using var connection = _factory.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { NoteId = noteId, LabelId = labelId, OwnerUserId = ownerUserId });
            return rows > 0;
        }

        public async Task<List<Label>> GetLabelsByNoteIdAsync(string noteId, int ownerUserId)
        {
            const string query = @"
                SELECT l.Id, l.Name, l.OwnerUserId, l.CreatedAt
                FROM Labels l
                INNER JOIN NoteLabels nl ON nl.LabelId = l.Id
                WHERE nl.NoteId = @NoteId AND nl.OwnerUserId = @OwnerUserId
                ORDER BY l.CreatedAt DESC;";

            using var connection = _factory.CreateConnection();
            var result = await connection.QueryAsync<Label>(query, new { NoteId = noteId, OwnerUserId = ownerUserId });
            return result.ToList();
        }
    }
}
