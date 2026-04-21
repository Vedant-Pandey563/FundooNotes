using LabelService.Application.DTOs;
using LabelService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Queries.GetLabelById
{
    public class GetLabelByIdHandler : IRequestHandler<GetLabelByIdQuery, LabelResponseDto?>
    {
        private readonly ILabelRepository _repo;

        public GetLabelByIdHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<LabelResponseDto?> Handle(GetLabelByIdQuery request, CancellationToken cancellationToken)
        {
            var label = await _repo.GetByIdAsync(request.Id, request.OwnerUserId);
            if (label == null) return null;

            return new LabelResponseDto(
                label.Id,
                label.Name,
                label.OwnerUserId,
                label.CreatedAt
            );
        }
    }
}
