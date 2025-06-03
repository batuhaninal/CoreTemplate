using Application.Abstractions.Repositories.Commons;
using Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Contexts;

namespace Persistence.Repositories.Commons
{
    public class WriteRepository<T> : IWriteRepository<T>
        where T : BaseEntity, new()
    {
        private readonly DbContext _context;

        public WriteRepository(DbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task AddRangeAsync(List<T> entities) => 
            await Table.AddRangeAsync(entities);

        public IDatabaseTransaction BeginTransaction() =>
            new DatabaseTransaction(_context);

        public async Task<T> CreateAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.Entity;
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
            T? entityForDelete = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            Remove(entityForDelete);
        }

        public async Task RemoveAsync(Guid id)
        {
            T? entityForDelete = await Table.FirstOrDefaultAsync(x => x.Id == id);
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
