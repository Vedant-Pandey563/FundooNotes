using LabelService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.Features.Label.Commands.DeleteLabel
{
    public class DeleteLabelHandler : IRequestHandler<DeleteLabelCommand, bool>
    {
        private readonly ILabelRepository _repo;

        public DeleteLabelHandler(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, request.OwnerUserId);
        }
    }
}
