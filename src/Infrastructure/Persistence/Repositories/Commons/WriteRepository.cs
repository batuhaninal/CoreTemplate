using Application.Abstractions.Repositories.Commons;
using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commons
{
    public class WriteRepository<T> : IWriteRepository<T>
        where T : BaseEntity, new()
    {
        private readonly TemplateContext _context;

        public WriteRepository(TemplateContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task AddRangeAsync(List<T> entities) => 
            await Table.AddRangeAsync(entities);

        public IDatabaseTransaction BeginTransaction() =>
            new DatabaseTransaction(_context);

        public async Task<Guid> CreateAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.Entity.Id;
        }

        public bool Remove(T? model)
        {
            if(model == null)
                return false;
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task RemoveAsync(string id)
        {
            T? entityForDelete = await Table.Where(x => x.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            Remove(entityForDelete);
        }

        public void RemoveRange(List<T> datas) => Table.RemoveRange(datas);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public T Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.Entity;
        }
    }
}
