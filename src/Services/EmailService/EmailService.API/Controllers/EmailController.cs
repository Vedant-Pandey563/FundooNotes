using Dapr;
using EmailService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        // This topic name must match the publisher exactly.
        [Topic("pubsub", "collaborator.added")]
        [HttpPost("collaborator-added")]
        public IActionResult HandleCollaboratorAdded([FromBody] CollaboratorAddedEvent data)
        {
            Console.WriteLine(
                $"[DAPR PUBSUB] Collaborator added event received. " +
                $"CollaboratorId={data.CollaboratorId}, NoteId={data.NoteId}, " +
                $"OwnerUserId={data.OwnerUserId}, CollaboratorUserId={data.CollaboratorUserId}");

            // Replace this with actual email sending later.
            return Ok();
        }
    }
}