using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CollaboratorService.Application.DTOs;
using CollaboratorService.Application.Features.Collaborators.Commands.AddCollaborator;
using CollaboratorService.Application.Features.Collaborators.Commands.RemoveCollaborator;
using CollaboratorService.Application.Features.Collaborators.Commands.UpdateCollaborator;
using CollaboratorService.Application.Features.Collaborators.Queries.GetAllCollaborators;
using CollaboratorService.Application.Features.Collaborators.Queries.GetCollaboratorById;


namespace CollaboratorService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollaboratorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCollaboratorsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCollaboratorByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCollaboratorDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            int ownerUserId = int.Parse(userIdClaim);

            var id = await _mediator.Send(new AddCollaboratorCommand(ownerUserId, dto));
            return Ok(new { Id = id, Message = "Collaborator added" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCollaboratorDto dto)
        {
            var updated = await _mediator.Send(new UpdateCollaboratorCommand(id, dto));

            if (!updated)
                return NotFound();

            return Ok("Collaborator updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var deleted = await _mediator.Send(new RemoveCollaboratorCommand(id));

            if (!deleted)
                return NotFound();

            return Ok("Collaborator removed");
        }
    }
}