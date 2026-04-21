using LabelService.Application.DTOs;
using LabelService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Queries.GetLabelByNoteId
{
    public class GetLabelsByNoteIdHandler : IRequestHandler<GetLabelsByNoteIdQuery, List<LabelResponseDto>>
    {
        private readonly ILabelRepository _repo;

        public GetLabelsByNoteIdHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<LabelResponseDto>> Handle(GetLabelsByNoteIdQuery request, CancellationToken cancellationToken)
        {
            var labels = await _repo.GetLabelsByNoteIdAsync(request.NoteId, request.OwnerUserId);

            return labels.Select(x => new LabelResponseDto(
                x.Id,
                x.Name,
                x.OwnerUserId,
                x.CreatedAt
            )).ToList();
        }
    }
}
