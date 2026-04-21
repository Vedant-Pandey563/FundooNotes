using System;
using System.Collections.Generic;
using System.Linq;
using LabelService.Domain.Entities;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Interfaces
{
    public interface ILabelRepository
    {
        Task<int> CreateAsync(Label label);
        Task<List<Label>> GetAllAsync(int ownerUserId);
        Task<Label?> GetByIdAsync(int id, int ownerUserId);
        Task<Label?> GetByNameAsync(string name, int ownerUserId);
        Task<bool> UpdateAsync(Label label);
        Task<bool> DeleteAsync(int id, int ownerUserId);

        Task<bool> AssignToNoteAsync(NoteLabel noteLabel);
        Task<bool> RemoveFromNoteAsync(string noteId, int labelId, int ownerUserId);
        Task<List<Label>> GetLabelsByNoteIdAsync(string noteId, int ownerUserId);
    }
}
