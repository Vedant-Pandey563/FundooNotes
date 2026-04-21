using CollaboratorService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator
{
    public record AddCollaboratorCommand(int OwnerUserId, AddCollaboratorDto Dto) : IRequest<int>;
}
