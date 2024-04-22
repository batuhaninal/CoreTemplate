namespace Application.Abstractions.Repositories.Commons
{
    public interface IDatabaseTransaction : IDisposable, IAsyncDisposable
    {
        void Commit();
        void Rollback();

        Task CommitAsync();
        Task RollbackAsync();
    }
}
