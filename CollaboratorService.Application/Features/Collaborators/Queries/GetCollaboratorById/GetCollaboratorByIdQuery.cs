using CollaboratorService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Queries.GetCollaboratorById
{
    public record GetCollaboratorByIdQuery(int Id) : IRequest<CollaboratorResponseDto?>;
}

