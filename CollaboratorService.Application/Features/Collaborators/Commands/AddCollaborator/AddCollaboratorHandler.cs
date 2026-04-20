using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using CollaboratorService.Application.Interfaces;
using CollaboratorService.Domain.Entites;

namespace CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator
{
    public class AddCollaboratorHandler : IRequestHandler<AddCollaboratorCommand, int>
    {
        private readonly ICollaboratorRepository _repo;

        public AddCollaboratorHandler(ICollaboratorRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var collaborator = new Collaborator
            {
                NoteId = request.Dto.NoteId,
                OwnerUserId = request.OwnerUserId,
                CollaboratorUserId = request.Dto.CollaboratorUserId,
                CreatedAt = DateTime.UtcNow
            };

            return await _repo.AddAsync(collaborator);
        }
    }
}
