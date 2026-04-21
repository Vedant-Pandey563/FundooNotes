using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    // Data coming from client during login
    public record LoginDto(
        string Email,
        string Password
    );
}
