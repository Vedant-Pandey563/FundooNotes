using LabelService.Application.DTOs;
using LabelService.Application.Features.Label.Commands.AssignLabel;
using LabelService.Application.Features.Label.Commands.CreateLabel;
using LabelService.Application.Features.Label.Commands.DeleteLabel;
using LabelService.Application.Features.Label.Commands.RemoveLabel;
using LabelService.Application.Features.Label.Commands.UpdateLabel;
using LabelService.Application.Features.Label.Queries.GetAllLabels;
using LabelService.Application.Features.Label.Queries.GetLabelById;
using LabelService.Application.Features.Label.Queries.GetLabelByNoteId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LabelService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LabelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private int GetOwnerUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrWhiteSpace(claim))
                throw new UnauthorizedAccessException("Missing userId claim.");

            return int.Parse(claim);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ownerUserId = GetOwnerUserId();
            var result = await _mediator.Send(new GetAllLabelsQuery(ownerUserId));
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ownerUserId = GetOwnerUserId();
            var result = await _mediator.Send(new GetLabelByIdQuery(id, ownerUserId));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("note/{noteId}")]
        public async Task<IActionResult> GetByNoteId(string noteId)
        {
            var ownerUserId = GetOwnerUserId();
            var result = await _mediator.Send(new GetLabelsByNoteIdQuery(noteId, ownerUserId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLabelDto dto)
        {
            var ownerUserId = GetOwnerUserId();
            var id = await _mediator.Send(new CreateLabelCommand(ownerUserId, dto));
            return Ok(new { Id = id, Message = "Label created" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateLabelDto dto)
        {
            var ownerUserId = GetOwnerUserId();
            var updated = await _mediator.Send(new UpdateLabelCommand(id, ownerUserId, dto));

            if (!updated)
                return NotFound();

            return Ok("Label updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ownerUserId = GetOwnerUserId();
            var deleted = await _mediator.Send(new DeleteLabelCommand(id, ownerUserId));

            if (!deleted)
                return NotFound();

            return Ok("Label deleted");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(AssignLabelDto dto)
        {
            var ownerUserId = GetOwnerUserId();
            var assigned = await _mediator.Send(new AssignLabelCommand(ownerUserId, dto));
            return assigned ? Ok("Label assigned to note") : BadRequest("Label already assigned");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(string noteId, int labelId)
        {
            var ownerUserId = GetOwnerUserId();
            var removed = await _mediator.Send(new RemoveLabelCommand(ownerUserId, noteId, labelId));

            if (!removed)
                return NotFound();

            return Ok("Label removed from note");
        }
    }
}
