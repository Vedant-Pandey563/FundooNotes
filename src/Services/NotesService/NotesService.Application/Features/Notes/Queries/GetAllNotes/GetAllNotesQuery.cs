using MediatR;
using NotesService.Application.DTOs;

public record GetAllNotesQuery(int UserId) : IRequest<List<NoteResponseDto>>;