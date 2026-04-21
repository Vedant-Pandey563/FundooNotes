using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    // Response returned after login (JWT token)
    public record AuthResponseDto(
        string Token,
        string Email
    );
}
