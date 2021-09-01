using System;
using System.Threading.Tasks;

namespace Advertising.Adverts.Services
{
    public class AdvertVisitService : IAdvertVisitService
    {
        private readonly IAdvertVisitRepository _advertVisitRepository;

        public AdvertVisitService(IAdvertVisitRepository advertVisitRepository)
        {
            _advertVisitRepository = advertVisitRepository;
        }

        public async Task Create(int advertId, string ipAddress, DateTime date)
        {
            await _advertVisitRepository.Insert(advertId, ipAddress, date);
        }
    }
}
