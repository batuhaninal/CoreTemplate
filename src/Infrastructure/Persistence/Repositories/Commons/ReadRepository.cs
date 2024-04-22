using Application.Abstractions.Repositories.Commons;
using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commons
{
    public class ReadRepository<T> : IReadRepository<T>
        where T : BaseEntity, new()
    {
        private readonly TemplateContext _context;

        public ReadRepository(TemplateContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool tracking = false) =>
            tracking ?
            await Table.AsTracking().FirstOrDefaultAsync(expression) :
            await Table.AsNoTracking().FirstOrDefaultAsync(expression);

        public async Task<T?> GetByIdAsync(string id, bool tracking = false) =>
            tracking ?
            await Table.AsTracking().FirstOrDefaultAsync(x=> x.Id == Guid.Parse(id)) :
            await Table.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == Guid.Parse(id));

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = false) =>
            tracking ?
            Table.AsQueryable().AsTracking().Where(expression) :
            Table.AsQueryable().AsNoTracking().Where(expression);

        public async Task<List<T>> ListAsync(bool tracking = false) => 
            tracking ?
            await Table.AsTracking().ToListAsync() :
            await Table.AsNoTracking().ToListAsync();
    }
}
