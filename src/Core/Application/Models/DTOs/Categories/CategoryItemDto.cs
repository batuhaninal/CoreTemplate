using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Categories
{
    public record CategoryItemDto(string CategoryId, string Title, DateTime CreatedDate);
}
