using MediatR;
using NotesService.Application.DTOs;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Features.Notes.Queries.GetNotesByLabel
{
    public class GetNotesByLabelHandler
        : IRequestHandler<GetNotesByLabelQuery, List<NoteResponseDto>>
    {
        private readonly INoteRepository _repo;

        public GetNotesByLabelHandler(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<NoteResponseDto>> Handle(
            GetNotesByLabelQuery request,
            CancellationToken cancellationToken)
        {
            var notes = await _repo.GetByLabelIdAsync(request.LabelId);

            return notes.Select(n => new NoteResponseDto(
                n.Id,
                n.Title,
                n.Description,  
                n.Color ?? "white",
                n.IsPinned,
                n.IsArchived,
                n.IsTrashed
            )).ToList();
        }
    }
}