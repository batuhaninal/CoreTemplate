using Application.Abstractions.Repositories.Categories;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        ICategoryReadRepository CategoryReadRepository { get; }
        ICategoryWriteRepository CategoryWriteRepository { get; }
        IDatabaseTransaction BeginTransaction();
        Task<int> SaveChangesAsync();
    }
}
