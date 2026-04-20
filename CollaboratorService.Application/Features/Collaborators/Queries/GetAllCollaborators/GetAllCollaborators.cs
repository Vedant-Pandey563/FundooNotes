using CollaboratorService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Queries.GetAllCollaborators
{
    public record GetAllCollaboratorsQuery() : IRequest<List<CollaboratorResponseDto>>;
}

