using LabelService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Queries.GetLabelByNoteId
{
    public record GetLabelsByNoteIdQuery(string NoteId, int OwnerUserId) : IRequest<List<LabelResponseDto>>;
}
