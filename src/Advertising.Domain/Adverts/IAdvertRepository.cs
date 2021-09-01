using Advertising.Application.Dto;
using Advertising.Domain.Repositories;
using System.Threading.Tasks;

namespace Advertising.Adverts
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<PagingResultDto<Advert>> GetAll(int? categoryId, decimal? minPrice, decimal? maxPrice, string gear, string fuel, string sort, string sortType, int pageSize, int page);
        Task<Advert> GetById(int id);
    }
}
