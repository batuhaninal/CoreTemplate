using Application.Abstractions.Repositories.Commons;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Categories
{
    public interface ICategoryReadRepository : IReadRepository<Category>
    {
    }
}
