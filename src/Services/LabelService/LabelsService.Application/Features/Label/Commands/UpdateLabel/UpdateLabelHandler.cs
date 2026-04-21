using LabelService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.UpdateLabel
{
    public class UpdateLabelHandler : IRequestHandler<UpdateLabelCommand, bool>
    {
        private readonly ILabelRepository _repo;

        public UpdateLabelHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id, request.OwnerUserId);
            if (existing == null) return false;

            existing.Name = request.Dto.Name.Trim();
            return await _repo.UpdateAsync(existing);
        }
    }
}
