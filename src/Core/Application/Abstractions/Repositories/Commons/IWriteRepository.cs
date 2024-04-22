using Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IWriteRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        Task<T> CreateAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task RemoveAsync(string id);
        bool Remove(T? model);
        void RemoveRange(List<T> datas);
        T Update(T entity);
        Task<int> SaveAsync();
        IDatabaseTransaction BeginTransaction();
    }
}
