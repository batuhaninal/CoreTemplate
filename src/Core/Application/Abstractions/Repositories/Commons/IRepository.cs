using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IRepository<T>
        where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }
    }
}
