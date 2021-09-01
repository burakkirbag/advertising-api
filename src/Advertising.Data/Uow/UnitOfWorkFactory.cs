using Advertising.Domain;
using Advertising.Domain.Uow;

namespace Advertising.Data.Uow
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDataContext _dbContext;

        public UnitOfWorkFactory(IDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dbContext);
        }
    }
}
