using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.API.Controllers
{
    [ApiController]
    public class EmailController : ControllerBase
    {
        [Topic("pubsub", "collaborator.added")]
        [HttpPost("collaborator-added")]
        public IActionResult HandleCollaboratorAdded([FromBody] dynamic data)
        {
            Console.WriteLine($"📧 Collaborator Added Email Sent: {data}");
            return Ok();
        }
    }
}