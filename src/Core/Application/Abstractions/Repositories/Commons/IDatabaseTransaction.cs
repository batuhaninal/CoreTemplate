using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
