using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Features.Notes.Queries.GetNotesByLabel
{
    public record GetNotesByLabelQuery(int LabelId) : IRequest<List<NoteResponseDto>>;
}