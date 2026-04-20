using CollaboratorService.Application.DTOs;
using CollaboratorService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Queries.GetCollaboratorById
{
    public class GetCollaboratorByIdHandler : IRequestHandler<GetCollaboratorByIdQuery, CollaboratorResponseDto?>
    {
        private readonly ICollaboratorRepository _repo;

        public GetCollaboratorByIdHandler(ICollaboratorRepository repo)
        {
            _repo = repo;
        }

        public async Task<CollaboratorResponseDto?> Handle(GetCollaboratorByIdQuery request, CancellationToken cancellationToken)
        {
            var collaborator = await _repo.GetByIdAsync(request.Id);

            if (collaborator == null)
                return null;

            return new CollaboratorResponseDto(
                collaborator.Id,
                collaborator.NoteId,
                collaborator.OwnerUserId,
                collaborator.CollaboratorUserId,
                collaborator.CreatedAt
            );
        }
    }
}
