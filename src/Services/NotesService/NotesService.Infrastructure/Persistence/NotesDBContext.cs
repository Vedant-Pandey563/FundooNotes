using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Driver;
using NotesService.Domain.Entities;
using NotesService.Infrastructure.Configurations;

namespace NotesService.Infrastructure.Persistence
{
    // This class handles MongoDB connection and collections
    public class NotesDbContext
    {
        private readonly IMongoDatabase _database;

        public NotesDbContext(MongoSettings settings)
        {
            // Create Mongo client using connection string
            var client = new MongoClient(settings.ConnectionString);

            // Get database instance
            _database = client.GetDatabase(settings.DatabaseName);

            // Store collection name
            NotesCollection = _database.GetCollection<Note>(settings.NotesCollectionName);
        }

        // MongoDB collection for Notes
        public IMongoCollection<Note> NotesCollection { get; }
    }
}