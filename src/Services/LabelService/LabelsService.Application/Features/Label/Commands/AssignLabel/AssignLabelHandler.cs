using LabelService.Application.Interfaces;
using LabelService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.AssignLabel
{
    public class AssignLabelHandler : IRequestHandler<AssignLabelCommand, bool>
    {
        private readonly ILabelRepository _repo;

        public AssignLabelHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(AssignLabelCommand request, CancellationToken cancellationToken)
        {
            var noteLabel = new NoteLabel
            {
                NoteId = request.Dto.NoteId,
                LabelId = request.Dto.LabelId,
                OwnerUserId = request.OwnerUserId,
                CreatedAt = DateTime.UtcNow
            };

            return await _repo.AssignToNoteAsync(noteLabel);
        }
    }
}
