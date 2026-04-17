using MediatR;
using NotesService.Application.Interfaces;

public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly INoteRepository _repo;

    public UpdateNoteHandler(INoteRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _repo.GetByIdAsync(request.Dto.Id);

        if (note == null) return false;

        // Update fields
        note.Title = request.Dto.Title;
        note.Description = request.Dto.Description;
        note.Color = request.Dto.Color;
        note.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(note);

        return true;
    }
}