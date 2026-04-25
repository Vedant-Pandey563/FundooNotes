using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Core MediatR library
// Provides:
// - IRequest<TResponse>
// - IRequestHandler<TRequest, TResponse>
using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Auth.Commands.Login
{
    // Login request → returns token
    //                         input                    output
    public record LoginCommand(LoginDto Dto) : IRequest<AuthResponseDto?>;
    // LoginCommand(LoginDto Dto):Primary constructor - Takes LoginDto as input

    // 3. : IRequest<AuthResponseDto?> - This tells MediatR: "This request expects a response of type AuthResponseDto?"
    // A handler must exist: of syntax  IRequestHandler<LoginCommand, AuthResponseDto?>

}
