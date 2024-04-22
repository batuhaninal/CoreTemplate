using Domain.Entities.Commons;
using System.Linq.Expressions;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IReadRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = false);
        Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool tracking = false);
        Task<T?> GetByIdAsync(string id, bool tracking = false);
        Task<List<T>> ListAsync(bool tracking = false);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>>? expression);
    }
}
