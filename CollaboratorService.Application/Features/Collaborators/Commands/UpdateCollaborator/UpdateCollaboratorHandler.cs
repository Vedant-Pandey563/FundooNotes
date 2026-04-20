using CollaboratorService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Commands.UpdateCollaborator
{
    public class UpdateCollaboratorHandler : IRequestHandler<UpdateCollaboratorCommand, bool>
    {
        private readonly ICollaboratorRepository _repo;

        public UpdateCollaboratorHandler(ICollaboratorRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing == null)
                return false;

            existing.NoteId = request.Dto.NoteId;
            existing.CollaboratorUserId = request.Dto.CollaboratorUserId;

            return await _repo.UpdateAsync(existing);
        }
    }
}
