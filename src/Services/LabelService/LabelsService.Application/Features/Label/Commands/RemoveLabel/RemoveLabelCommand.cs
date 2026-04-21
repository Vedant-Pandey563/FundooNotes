using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.RemoveLabel
{
    public record RemoveLabelCommand(int OwnerUserId, string NoteId, int LabelId) : IRequest<bool>;
}
