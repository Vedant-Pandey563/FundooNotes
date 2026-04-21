using MediatR;


namespace NotesService.Application.Features.Notes.Commands.DeleteNote
{
    public record DeleteNoteCommand(string Id) : IRequest<bool>;
}