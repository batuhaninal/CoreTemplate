using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Products
{
    public record CreateProductDto
    {
        public string Title { get; init; } = null!;
        public decimal Price { get; init; }
    }
}
