using Advertising.Adverts.Dto;
using Advertising.Application.Dto;
using Advertising.Application.Queries;

namespace Advertising.Adverts.Queries.GetAdverts
{
    public class GetAdvertsQuery : QueryBase<PagingResultDto<AdvertDto>>
    {
        public int? CategoryId { get; }
        public decimal? MinPrice { get; }
        public decimal? MaxPrice { get; }
        public string Gear { get; }
        public string Fuel { get; }

        public GetAdvertsQuery(int? categoryId, decimal? minPrice, decimal? maxPrice, string gear, string fuel, int page, int pageSize, string sort, string sortType) : base(page, pageSize, sort, sortType)
        {
            CategoryId = categoryId;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Gear = gear;
            Fuel = fuel;
        }
    }
}
