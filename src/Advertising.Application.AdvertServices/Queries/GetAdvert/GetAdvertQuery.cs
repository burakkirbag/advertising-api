using Advertising.Adverts.Dto;
using Advertising.Application.Queries;

namespace Advertising.Adverts.Queries.GetAdvert
{
    public class GetAdvertQuery : IQuery<AdvertDetailDto>
    {
        public int Id { get; }

        public GetAdvertQuery(int id)
        {
            Id = id;
        }
    }
}
