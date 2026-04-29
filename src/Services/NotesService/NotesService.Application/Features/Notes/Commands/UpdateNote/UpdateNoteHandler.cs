using MediatR;
using NotesService.Application.Interfaces;

namespace NotesService.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly INoteRepository _repo;
        private readonly ICacheService _cache;

        public UpdateNoteHandler(INoteRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _repo.GetByIdAsync(request.Dto.Id);

            if (note == null)
                return false;

            note.Title = request.Dto.Title;
            note.Description = request.Dto.Description;
            note.Color = request.Dto.Color;

            await _repo.UpdateAsync(note);

            // invalidate cache
            string cacheKey = $"notes_user_{note.UserId}";
            await _cache.RemoveAsync(cacheKey);

            Console.WriteLine($"Cache invalidated after UPDATE: {cacheKey}");

            return true;
        }
    }
}