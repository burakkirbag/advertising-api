using System;
using System.Threading.Tasks;

namespace Advertising.Adverts.Services
{
    public interface IAdvertVisitService
    {
        Task Create(int advertId, string ipAddress, DateTime date);
    }
}
