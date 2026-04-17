using MediatR;
using NotesService.Application.Interfaces;

public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly INoteRepository _repo;

    public DeleteNoteHandler(INoteRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _repo.GetByIdAsync(request.Id);

        if (note == null) return false;

        await _repo.DeleteAsync(request.Id);

        return true;
    }
}