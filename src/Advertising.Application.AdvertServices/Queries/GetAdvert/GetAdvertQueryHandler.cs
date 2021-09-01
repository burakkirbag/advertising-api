using Advertising.Adverts.Dto;
using Advertising.Application.Queries;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Adverts.Queries.GetAdvert
{
    public class GetAdvertQueryHandler : QueryHandlerBase<GetAdvertQuery, AdvertDetailDto>
    {
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertQueryHandler(IMapper mapper, IAdvertRepository advertRepository) : base(mapper)
        {
            _advertRepository = advertRepository;
        }

        public override async Task<AdvertDetailDto> Handle(GetAdvertQuery request, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetById(request.Id);
            return Mapper.Map<AdvertDetailDto>(advert);
        }
    }
}
