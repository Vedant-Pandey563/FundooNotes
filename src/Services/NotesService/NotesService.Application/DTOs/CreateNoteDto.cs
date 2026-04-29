using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Application.DTOs
{
    //record for note creation
    //record better for data storage , immutable and easy obj compare
    // Record with init-only properties (clean + flexible)
    public record CreateNoteDto
    {
        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public List<int>? LabelIds { get; init; } // optional

        public string? Color { get; init; }
    }

}
