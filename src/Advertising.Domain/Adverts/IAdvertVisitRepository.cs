using Advertising.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Advertising.Adverts
{
    public interface IAdvertVisitRepository : IRepository<AdvertVisit>
    {
        public Task<int> Insert(int advertId, string ipAddress, DateTime date);
    }
}
