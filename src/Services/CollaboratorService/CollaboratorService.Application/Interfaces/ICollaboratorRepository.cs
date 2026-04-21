using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaboratorService.Domain.Entites;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<int> AddAsync(Collaborator collaborator);
        Task<List<Collaborator>> GetAllAsync();
        Task<Collaborator?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Collaborator collaborator);
        Task<bool> RemoveAsync(int id);
    }
}
