using Advertising.Adverts.Dto;
using Advertising.Application.Dto;
using Advertising.Application.Queries;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Adverts.Queries.GetAdverts
{
    public class GetAdvertsQueryHandler : QueryHandlerBase<GetAdvertsQuery, PagingResultDto<AdvertDto>>
    {
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertsQueryHandler(IMapper mapper, IAdvertRepository advertRepository) : base(mapper)
        {
            _advertRepository = advertRepository;
        }

        public override async Task<PagingResultDto<AdvertDto>> Handle(GetAdvertsQuery request, CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetAll(request.CategoryId, request.MinPrice, request.MaxPrice, request.Gear, request.Fuel, request.Sort, request.SortType, request.PageSize, request.Page);
            return Mapper.Map<PagingResultDto<AdvertDto>>(adverts);
        }
    }
}
