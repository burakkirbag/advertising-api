using Advertising.Domain;
using Advertising.Domain.Uow;
using System;
using System.Data;

namespace Advertising.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dbContext;

        public UnitOfWork(IDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Id => _dbContext.Id;

        public virtual IDbConnection Connection => _dbContext.Connection;

        public virtual IDbTransaction Transaction => _dbContext.Transaction;

        public virtual void Begin() => _dbContext.BeginTransaction();

        public virtual void Commit() => _dbContext.Commit();

        public void Rollback() => _dbContext.Rollback();

        public virtual void Dispose()
        {
            if (_dbContext.Transaction != null)
                _dbContext.Rollback();
        }
    }
}
