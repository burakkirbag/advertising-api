using System;
using System.Data;

namespace Advertising.Domain
{
    public interface IDataContext : IDisposable
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void OpenConnection();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
