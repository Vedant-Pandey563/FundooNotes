using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.DTOs
{
    public record AssignLabelDto(string NoteId, int LabelId);
}
