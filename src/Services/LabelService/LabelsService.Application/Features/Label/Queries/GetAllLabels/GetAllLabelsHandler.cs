using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelService.Application.DTOs;
using LabelService.Application.Interfaces;
using MediatR;

namespace LabelService.Application.Features.Label.Queries.GetAllLabels
{
    public class GetAllLabelsHandler : IRequestHandler<GetAllLabelsQuery, List<LabelResponseDto>>
    {
        private readonly ILabelRepository _repo;

        public GetAllLabelsHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<LabelResponseDto>> Handle(GetAllLabelsQuery request, CancellationToken cancellationToken)
        {
            var labels = await _repo.GetAllAsync(request.OwnerUserId);

            return labels.Select(x => new LabelResponseDto(
                x.Id,
                x.Name,
                x.OwnerUserId,
                x.CreatedAt
            )).ToList();
        }
    }
}
