using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesService.Application.DTOs;
using NotesService.Application.Features.Notes.Commands.CreateNote;
using Microsoft.AspNetCore.Authorization;
using NotesService.Application.Features.Notes.Commands.DeleteNote;
using NotesService.Application.Features.Notes.Commands.UpdateNote;
using NotesService.Application.Features.Notes.Queries.GetAllNotes;

namespace NotesService.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        // MediatR is injected via constructor
        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new note
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            // If NotesService expects int:
            int userId = int.Parse(userIdClaim);

            // Send command to handler via MediatR
            var noteId = await _mediator.Send(
                new CreateNoteCommand(userId, dto)
            );

            // Return response
            return Ok(new
            {
                Message = "Note created successfully",
                Id = noteId
            });
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            // If NotesService expects int:
            int userId = int.Parse(userIdClaim);

            var notes = await _mediator.Send(new GetAllNotesQuery(userId));

            return Ok(notes);
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update(UpdateNoteDto dto)
        {
            var result = await _mediator.Send(new UpdateNoteCommand(dto));

            if (!result) return NotFound();

            return Ok("Updated successfully");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteNoteCommand(id));

            if (!result) return NotFound();

            return Ok("Deleted successfully");
        }
    }
}