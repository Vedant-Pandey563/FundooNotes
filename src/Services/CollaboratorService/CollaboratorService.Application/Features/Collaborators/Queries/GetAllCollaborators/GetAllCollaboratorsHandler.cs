using CollaboratorService.Application.DTOs;
using CollaboratorService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Queries.GetAllCollaborators
{
    public class GetAllCollaboratorsHandler : IRequestHandler<GetAllCollaboratorsQuery, List<CollaboratorResponseDto>>
    {
        private readonly ICollaboratorRepository _repo;

        public GetAllCollaboratorsHandler(ICollaboratorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CollaboratorResponseDto>> Handle(GetAllCollaboratorsQuery request, CancellationToken cancellationToken)
        {
            var collaborators = await _repo.GetAllAsync();

            return collaborators.Select(x => new CollaboratorResponseDto(
                x.Id,
                x.NoteId,
                x.OwnerUserId,
                x.CollaboratorUserId,
                x.CreatedAt
            )).ToList();
        }
    }
}
