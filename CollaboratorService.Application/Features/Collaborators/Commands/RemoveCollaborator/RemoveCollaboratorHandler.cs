using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using CollaboratorService.Application.Interfaces;

namespace CollaboratorService.Application.Features.Collaborators.Commands.RemoveCollaborator
{
    public class RemoveCollaboratorHandler : IRequestHandler<RemoveCollaboratorCommand, bool>
    {
        private readonly ICollaboratorRepository _repo;

        public RemoveCollaboratorHandler(ICollaboratorRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(RemoveCollaboratorCommand request, CancellationToken cancellationToken)
        {
            return await _repo.RemoveAsync(request.Id);
        }
    }
}
