using Advertising.Application.Dto;

namespace Advertising.Api.Models.Requests
{
    public class GetAdvertsRequest : PagingRequestDto
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Gear { get; set; }
        public string Fuel { get; set; }
        /// <summary>
        /// price, year, km
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// asc, desc
        /// </summary>
        public string SortType { get; set; }
    }
}
