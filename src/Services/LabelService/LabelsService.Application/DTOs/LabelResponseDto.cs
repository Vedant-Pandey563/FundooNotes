using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelService.Application.DTOs
{
    public record LabelResponseDto(
        int Id,
        string Name,
        int OwnerUserId,
        DateTime CreatedAt
    );
}
