using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Auths
{
    public record LoginDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
