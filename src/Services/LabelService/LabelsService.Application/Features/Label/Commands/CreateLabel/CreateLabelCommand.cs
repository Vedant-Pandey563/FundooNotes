using LabelService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LabelService.Application.Features.Label.Commands.CreateLabel
{
    public record CreateLabelCommand(int OwnerUserId, CreateLabelDto Dto) : IRequest<int>;
}
