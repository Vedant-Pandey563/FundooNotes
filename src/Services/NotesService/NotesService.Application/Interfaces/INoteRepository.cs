using System;
using System.Collections.Generic;
using System.Text;
using NotesService.Domain.Entities;

namespace NotesService.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<string> CreateAsync(Note note);
        Task<List<Note>> GetByUserIdAsync(int userId);
        Task<Note?> GetByIdAsync(string id);
        Task UpdateAsync(Note note);
        Task DeleteAsync(string id);
    }
}
