using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaboratorService.Application.DTOs
{
    public record CollaboratorResponseDto(
        int Id,
        string NoteId,
        int OwnerUserId,
        int CollaboratorUserId,
        DateTime CreatedAt
    );
}
