using LabelService.Application.Interfaces;
using LabelService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.CreateLabel
{
    public class CreateLabelHandler : IRequestHandler<CreateLabelCommand, int>
    {
        private readonly ILabelRepository _repo;

        public CreateLabelHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByNameAsync(request.Dto.Name, request.OwnerUserId);
            if (existing != null)
                throw new InvalidOperationException("Label already exists.");

            var label = new LabelService.Domain.Entities.Label
            {
                Name = request.Dto.Name.Trim(),
                OwnerUserId = request.OwnerUserId,
                CreatedAt = DateTime.UtcNow
            };

            return await _repo.CreateAsync(label);
        }
    }
}
