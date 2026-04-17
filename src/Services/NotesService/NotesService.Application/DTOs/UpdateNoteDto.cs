using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Application.DTOs
{
    public record UpdateNoteDto(
    string Id,
    string Title,
    string Description,
    string? Color
);
}
