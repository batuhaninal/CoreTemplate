using Domain.Entities.Commons;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IWriteRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        Task<Guid> CreateAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task RemoveAsync(string id);
        bool Remove(T? model);
        void RemoveRange(List<T> datas);
        T Update(T entity);
        Task<int> SaveAsync();
        IDatabaseTransaction BeginTransaction();
    }
}
