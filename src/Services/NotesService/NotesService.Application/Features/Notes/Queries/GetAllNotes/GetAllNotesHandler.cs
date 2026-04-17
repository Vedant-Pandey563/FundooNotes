using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Application.DTOs;

public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, List<NoteResponseDto>>
{
    private readonly INoteRepository _repo;

    public GetAllNotesHandler(INoteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<NoteResponseDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _repo.GetByUserIdAsync(Convert.ToInt32(request.UserId));

        // Convert Domain → DTO
        return notes.Select(n => new NoteResponseDto(
            n.Id,
            n.Title,
            n.Description,
            n.Color,
            n.IsPinned,
            n.IsArchived,
            n.IsTrashed
        )).ToList();
    }
}