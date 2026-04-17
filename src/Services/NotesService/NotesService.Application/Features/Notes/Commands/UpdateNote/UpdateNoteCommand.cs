using MediatR;
using NotesService.Application.DTOs;

public record UpdateNoteCommand(UpdateNoteDto Dto) : IRequest<bool>;