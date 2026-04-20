using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Auth.Commands.Login
{
    // Login request → returns token
    //                         input                    output
    public record LoginCommand(LoginDto Dto) : IRequest<AuthResponseDto?>;
}
