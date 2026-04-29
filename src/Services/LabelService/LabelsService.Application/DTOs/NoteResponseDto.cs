namespace LabelService.Application.DTOs
{
    // Local DTO for reading the NotesService response.
    // This avoids taking a direct project reference to NotesService.Application.
    public record NoteResponseDto(
        string Id,
        string Title,
        string Description,
        string? Color,
        bool IsPinned,
        bool IsArchived,
        bool IsTrashed
    );
}
