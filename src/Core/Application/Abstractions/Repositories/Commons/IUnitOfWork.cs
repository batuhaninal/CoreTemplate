using Application.Abstractions.Repositories.Categories;
using Application.Abstractions.Repositories.Products;

namespace Application.Abstractions.Repositories.Commons
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        ICategoryReadRepository CategoryReadRepository { get; }
        ICategoryWriteRepository CategoryWriteRepository { get; }
        IProductReadRepository ProductReadRepository { get; }
        IProductWriteRepository ProductWriteRepository { get; }
        IDatabaseTransaction BeginTransaction();
        Task<int> SaveChangesAsync();
    }
}
