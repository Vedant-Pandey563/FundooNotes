using MediatR;
using NotesService.Application.Interfaces;

public class AddLabelToNoteHandler
    : IRequestHandler<AddLabelToNoteCommand, bool>
{
    private readonly INoteRepository _repo;

    public AddLabelToNoteHandler(INoteRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(AddLabelToNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _repo.GetByIdAsync(request.NoteId);

        if (note == null)
            return false;

        if (!note.LabelIds.Contains(request.LabelId))
        {
            note.LabelIds.Add(request.LabelId);
            await _repo.UpdateAsync(note);
        }

        return true;
    }
}