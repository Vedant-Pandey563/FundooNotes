using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Domain.Entities;

namespace NotesService.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, string>
    {
        private readonly INoteRepository _repo;
        private readonly ICacheService _cache;

        public CreateNoteHandler(INoteRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<string> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            // Create note with correct UserId
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Dto.Title,
                Description = request.Dto.Description,
                Color = request.Dto.Color,
                LabelIds = request.Dto.LabelIds ?? new List<int>()
            };

            var noteId = await _repo.CreateAsync(note);

            //  CRITICAL FIX — invalidate cache
            string cacheKey = $"notes_user_{request.UserId}";
            await _cache.RemoveAsync(cacheKey);

            Console.WriteLine($"Cache invalidated after CREATE: {cacheKey}");

            return noteId;
        }
    }
}