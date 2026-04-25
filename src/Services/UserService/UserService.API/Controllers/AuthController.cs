using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Application.DTOs;
using UserService.Application.Features.Auth.Commands.Login;
using UserService.Application.Features.Auth.Commands.Register;


namespace UserService.API.Controllers
{
    // Marks this class as an ASP.NET Core Web API controller
    [ApiController]
    // Defines the base route for all endpoints in this controller
    [Route("api/[controller]")]

    // Declares a public controller class named AuthController
    public class AuthController : ControllerBase
    {
        // Declares a private, read-only field of type IMediator
        // It implements the Mediator pattern → decouples controller from business logic
        private readonly IMediator _mediator;

        //Mediatr dependency inject
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // This attribute allows access to this endpoint WITHOUT authentication
        [AllowAnonymous]
        //register new user
        // Maps this method to HTTP POST requests at route: "api/auth/register"
        [HttpPost("register")]

        // async  enables non-blocking I/O 
        // Task<IActionResult> → returns HTTP response asynchronously
        // binds incoming json body to dto 
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            //Create a new RegisterCommand object
            // Mediator → finds RegisterCommandHandler → executes it
            var result = await _mediator.Send(new RegisterCommand(dto));

            return Ok(new
            {
                Message = "User successfull registerd",
                Data = result
            });
        }


        //login new user
        [AllowAnonymous]
        // Maps this method to HTTP POST requests at route: "api/auth/login"
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(new LoginCommand(dto));

            // Checks if authentication failed
            if (result == null)
            {
                // Returns HTTP 401 Unauthorized
                return Unauthorized("Invalid email or password");
            }

            return Ok(result);
        }

        // Enforces authentication for this endpoint

        [Authorize]
        // Maps this method to GET requests at: api/auth/me 
        // Returns currently authenticated user's identity info
        [HttpGet("me")]
        // Only reading in-memory claims from JWT , thats ehy no async
        public IActionResult Me()
        {
            return Ok(new
            {
                // "User" is: HttpContext.User (ClaimsPrincipal)
                // extract userid from jwt midware
                UserId = User.FindFirst("userId")?.Value,
                Email = User.FindFirst(ClaimTypes.Email)?.Value
            });
        }
    }
}
