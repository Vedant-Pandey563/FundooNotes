using LabelService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Queries.GetAllLabels
{
    public record GetAllLabelsQuery(int OwnerUserId) : IRequest<List<LabelResponseDto>>;
}
