using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Users
{
    public record UpdateUserDto
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
