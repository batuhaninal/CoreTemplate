using Application.Abstractions.Repositories.Commons;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Products
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
    }
}
