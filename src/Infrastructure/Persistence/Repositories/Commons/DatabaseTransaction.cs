using Application.Abstractions.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commons
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        public DatabaseTransaction(DbContext dbContext)
        {
            _dbContextTransaction = dbContext.Database.BeginTransaction();
        }

        public void Commit() => _dbContextTransaction.Commit();

        public async Task CommitAsync() => await _dbContextTransaction.CommitAsync();

        public void Dispose() => _dbContextTransaction.Dispose();

        public async ValueTask DisposeAsync() => await _dbContextTransaction.DisposeAsync();

        public void Rollback() => _dbContextTransaction.Rollback();

        public async Task RollbackAsync() => await _dbContextTransaction.RollbackAsync();
    }
}
