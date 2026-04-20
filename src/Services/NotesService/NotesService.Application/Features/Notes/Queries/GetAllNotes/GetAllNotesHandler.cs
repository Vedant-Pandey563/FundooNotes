using MediatR;
using NotesService.Application.Interfaces;
using NotesService.Application.DTOs;


public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, List<NoteResponseDto>>
{
    private readonly INoteRepository _repo;
    private readonly ICacheService _cache;

    public GetAllNotesHandler(INoteRepository repo, ICacheService cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<List<NoteResponseDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"notes_user_{request.UserId}";

        //  Try cache first
        var cached = await _cache.GetAsync<List<NoteResponseDto>>(cacheKey);

        if (cached != null)
        {
            Console.WriteLine($"Cache hit: {cacheKey}");
            return cached;
        }

        Console.WriteLine($"Cache miss: {cacheKey}");

        var notes = await _repo.GetByUserIdAsync(request.UserId);

        var result = notes.Select(n => new NoteResponseDto(
            n.Id,
            n.Title,
            n.Description,
            n.Color,
            n.IsPinned,
            n.IsArchived,
            n.IsTrashed
        )).ToList();

        //  Store in cache
        await _cache.SetAsync(cacheKey, result);
        Console.WriteLine($"Cache set: {cacheKey}");

        return result;
    }
}