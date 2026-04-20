using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Auth.Commands.Register
{
    // Command sent from controller → handler
    // ireq use register cmd to find required handler
    public record RegisterCommand(RegisterDto Dto) : IRequest<bool>;

}
