using Advertising.Adverts;
using Advertising.Domain;
using Dapper;
using System;
using System.Threading.Tasks;

namespace Advertising.Data.Repositories
{
    public class AdvertVisitRepository : RepositoryBase<AdvertVisit>, IAdvertVisitRepository
    {
        public AdvertVisitRepository(IDataContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Insert(int advertId, string ipAddress, DateTime date)
        {
            var result = await DbContext.Connection.ExecuteAsync("INSERT INTO advert_visits (advert_id, ip_address, visit_date) VALUES(@advertId, @ipAddress, @date)", new { advertId, ipAddress, date });
            return result;
        }
    }
}
