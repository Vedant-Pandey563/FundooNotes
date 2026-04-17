using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;
using NotesService.Infrastructure.Persistence;

namespace NotesService.Infrastructure.Repositories
{
    // Implements Application layer interface
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteRepository(NotesDbContext context)
        {
            _notes = context.NotesCollection;
        }

        // Create a new note

        public async Task<string> CreateAsync(Note note)
        {
            await _notes.InsertOneAsync(note);

            // Mongo uses ObjectId normally, but since we used int,
            // you may later replace with string Id (recommended)
            return note.Id;
        }

        // Get all notes by user
        public async Task<List<Note>> GetByUserIdAsync(int userId)
        {
            return await _notes
                .Find(n => (n.UserId) == userId)
                .ToListAsync();
        }

        // Get note by Id
        public async Task<Note?> GetByIdAsync(string id)
        {
            return await _notes
                .Find(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        // Update note
        public async Task UpdateAsync(Note note)
        {
            await _notes.ReplaceOneAsync(n => n.Id == note.Id, note);
        }

        // Delete note
        public async Task DeleteAsync(string id)
        {
            await _notes.DeleteOneAsync(n => n.Id == id);
        }
    }
}
