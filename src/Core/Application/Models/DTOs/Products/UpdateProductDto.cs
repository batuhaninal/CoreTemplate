using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Products
{
    public record UpdateProductDto
    {
        public Guid ProductId { get; init; }
        public string? Title { get; init; }
        public decimal Price { get; init; }
    }
}
