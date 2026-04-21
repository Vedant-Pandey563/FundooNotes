using LabelService.Application.DTOs;
using MediatR;

namespace LabelService.Application.Features.Label.Commands.UpdateLabel
{
    public record UpdateLabelCommand(int Id, int OwnerUserId, UpdateLabelDto Dto) : IRequest<bool>;
}