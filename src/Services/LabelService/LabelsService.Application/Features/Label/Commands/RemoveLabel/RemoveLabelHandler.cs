using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelService.Application.Interfaces;
using MediatR;

namespace LabelService.Application.Features.Label.Commands.RemoveLabel
{
    public class RemoveLabelHandler : IRequestHandler<RemoveLabelCommand, bool>
    {
        private readonly ILabelRepository _repo;

        public RemoveLabelHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(RemoveLabelCommand request, CancellationToken cancellationToken)
        {
            return await _repo.RemoveFromNoteAsync(request.NoteId, request.LabelId, request.OwnerUserId);
        }
    }
}
