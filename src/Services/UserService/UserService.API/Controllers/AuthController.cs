using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Application.DTOs;
using UserService.Application.Features.Auth.Commands.Login;
using UserService.Application.Features.Auth.Commands.Register;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        // IMediator is injected so the controller stays thin
        // and all business logic remains in handlers.
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var created = await _mediator.Send(new RegisterCommand(dto));

            // If the handler returns false, the email already exists.
            if (!created)
            {
                return Conflict(new
                {
                    Message = "A user with this email already exists."
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                Message = "User registered successfully."
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(new LoginCommand(dto));

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = User.FindFirst("userId")?.Value,
                Email = User.FindFirst(ClaimTypes.Email)?.Value
            });
        }
    }
}