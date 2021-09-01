using Advertising.Domain;
using Advertising.Domain.Entities;
using Advertising.Domain.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertising.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase, IEntity, new()
    {
        protected IDataContext DbContext { get; private set; }

        public RepositoryBase(IDataContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<List<T>> QueryAsync<T>(string query, object parameters = null)
        {
            var queryTask = DbContext.Connection.QueryAsync<T>(query, parameters);
            var data = await queryTask;
            return data.ToList();
        }

        public virtual async Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            var queryTask = DbContext.Connection.QuerySingleAsync<T>(query, parameters);
            var data = await queryTask;
            return data;
        }

        public virtual async Task<T> QueryFirstAsync<T>(string query, object parameters = null)
        {
            var queryTask = DbContext.Connection.QueryFirstAsync<T>(query, parameters);
            var data = await queryTask;
            return data;
        }

        public virtual async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            var queryTask = DbContext.Connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            var data = await queryTask;
            return data;
        }

        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            var queryTask = DbContext.Connection.QueryFirstOrDefaultAsync<T>(query, parameters);
            var data = await queryTask;
            return data;
        }

        #region [IDisposable implementation]
        public virtual void Dispose()
        {

        }
        #endregion
    }
}
