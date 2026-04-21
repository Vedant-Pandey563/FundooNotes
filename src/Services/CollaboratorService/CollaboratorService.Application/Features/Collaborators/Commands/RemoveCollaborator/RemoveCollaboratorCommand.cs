using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.Features.Collaborators.Commands.RemoveCollaborator
{
    public record RemoveCollaboratorCommand(int Id) : IRequest<bool>;
}
