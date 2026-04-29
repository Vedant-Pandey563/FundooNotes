using System;

namespace CollaboratorService.Domain.Entites
{
    // Domain entity for a collaborator entry.
    // CreatedAt is initialized here so inserts never send DateTime.MinValue to SQL.
    public class Collaborator
    {
        public int Id { get; set; }

        public string NoteId { get; set; } = string.Empty;

        public int OwnerUserId { get; set; }

        public int CollaboratorUserId { get; set; }

        // SQL Server DATETIME cannot store DateTime.MinValue.
        // UtcNow is safe and consistent for audit fields.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}