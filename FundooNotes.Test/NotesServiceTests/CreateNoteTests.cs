using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesService.Application.Features.Notes.Commands.CreateNote;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;

namespace FundooNotes.Tests.NotesServiceTests
{
    [TestClass]
    public class CreateNoteTests
    {
        private class FakeNoteRepository : INoteRepository
        {
            //  match interface EXACTLY
            public Task<string> CreateAsync(Note note)
                => Task.FromResult("note-id-1"); // Mongo returns string id

            public Task<List<Note>> GetByUserIdAsync(int userId)
                => Task.FromResult(new List<Note>());

            public Task<Note?> GetByIdAsync(string id)
                => Task.FromResult<Note?>(null);

            public Task UpdateAsync(Note note)
                => Task.CompletedTask;

            public Task DeleteAsync(string id)
                => Task.CompletedTask;
        }

        [TestMethod]
        public async Task CreateNote_Should_Work()
        {
            var repo = new FakeNoteRepository();
            var handler = new CreateNoteHandler(repo);

            // ✅ FIX: Use DTO (not raw params)
            var dto = new CreateNoteDto("Title", "Content",null);

            var command = new CreateNoteCommand(1, dto);

            var result = await handler.Handle(command, CancellationToken.None);

            // handler returns string (noteId)
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}