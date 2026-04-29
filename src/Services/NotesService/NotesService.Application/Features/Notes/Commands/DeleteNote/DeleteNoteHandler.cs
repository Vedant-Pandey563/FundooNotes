using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Features.Notes.Commands.DeleteNote
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly INoteRepository _repo;
        private readonly ICacheService _cache;

        public DeleteNoteHandler(INoteRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _repo.GetByIdAsync(request.Id);

            if (note == null)
                return false;

            await _repo.DeleteAsync(request.Id);

            // invalidate cache
            string cacheKey = $"notes_user_{note.UserId}";
            await _cache.RemoveAsync(cacheKey);

            Console.WriteLine($"Cache invalidated after DELETE: {cacheKey}");

            return true;
        }
    }
}