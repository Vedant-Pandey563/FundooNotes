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

        //Mediatr inject
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        //register new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _mediator.Send(new RegisterCommand(dto));

            return Ok(new
                {
                    Message = "User successfull registerd"
            });
        }


        //login new user
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(new LoginCommand(dto));

            if(result == null)
            {
                return Unauthorized("Invalid email or password");
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
