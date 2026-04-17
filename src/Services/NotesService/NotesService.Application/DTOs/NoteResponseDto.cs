using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Application.DTOs
{
    public record NoteResponseDto(
        string Id,
        string Title,
        string Description,
        string Color,
        bool IsPinned,
        bool IsArchived,
        bool IsTrashed);
}
