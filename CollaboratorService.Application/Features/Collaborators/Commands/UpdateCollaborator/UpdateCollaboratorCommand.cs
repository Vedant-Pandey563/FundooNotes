using CollaboratorService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Commands.UpdateCollaborator
{
    public record UpdateCollaboratorCommand(int Id, UpdateCollaboratorDto Dto) : IRequest<bool>;
}
