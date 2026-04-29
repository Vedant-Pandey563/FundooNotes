using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesService.Domain.Entities
{
    public class Note
    {
        // core business model

        // MongoDB primary key
        [BsonId] // Marks this as the document ID
        [BsonRepresentation(BsonType.ObjectId)] // Stores as ObjectId but uses string in C#
        public string Id { get; set; } = string.Empty;
        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description {  get; set; } = string.Empty;

        public string? Color {  get; set; }

        public bool IsPinned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsTrashed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt {  get; set; }
        public List<int> LabelIds { get; set; } = new();

    }
}
