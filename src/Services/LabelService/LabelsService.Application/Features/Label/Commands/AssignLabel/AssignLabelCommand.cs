using LabelService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.AssignLabel
{
    public record AssignLabelCommand(int OwnerUserId, AssignLabelDto Dto) : IRequest<bool>;
}
