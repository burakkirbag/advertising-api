using Advertising.Configuration;
using Advertising.Domain;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace Advertising.Data
{
    public class DbContext : IDataContext
    {
        #region [.ctor]
        public DbContext(IOptions<DatabaseConfig> configuration)
        {
            _configuration = configuration.Value;

            Id = Guid.NewGuid();
            _connection = CreateConnection();
        }
        #endregion

        #region [private members]
        private readonly DatabaseConfig _configuration;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        #endregion

        #region [props]
        public virtual Guid Id { get; }

        public virtual IDbConnection Connection
        {
            get
            {
                if (_connection == null) _connection = CreateConnection();

                OpenConnection();
                return _connection;
            }
        }

        public virtual IDbTransaction Transaction => _transaction;
        #endregion

        #region [IDataContext implementation]
        public virtual void OpenConnection()
        {
            if (_connection != null &&
                _connection.State != ConnectionState.Open &&
                _connection.State != ConnectionState.Connecting)
                _connection.Open();
        }

        public virtual void BeginTransaction()
        {
            if (_transaction != null)
                throw new AdvertisingException("Transaction is already started.");

            _transaction = _connection.BeginTransaction();
        }

        public virtual void Commit()
        {
            if (_transaction == null)
                throw new AdvertisingException("No transaction started.");

            _transaction.Commit();
            _transaction = null;
        }

        public virtual void Rollback()
        {
            if (_transaction == null)
                throw new AdvertisingException("No transaction started.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
        #endregion

        #region [IDisposable implementation]
        public virtual void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }

            _connection = null;
            _transaction = null;
        }
        #endregion

        #region [helpers]
        private IDbConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(_configuration.ConnectionString))
                throw new AdvertisingException($"Database connection string is invalid.");

            var connection = new NpgsqlConnection(_configuration.ConnectionString);

            return connection;
        }
        #endregion
    }
}
