using Advertising.Domain.Entities;
using System;

namespace Advertising.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity, new()
    {

    }
}
