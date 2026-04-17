using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Application.DTOs
{
    //record for note creation
    //record better for data storage , immutable and easy obj compare
    public record CreateNoteDto(
        string Title,
        string Description,
        string? Color);
}
