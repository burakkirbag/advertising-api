using Advertising.Adverts.Dto;
using Advertising.Application.Dto;
using AutoMapper;

namespace Advertising.Adverts
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Advert, AdvertDto>();
            CreateMap<Advert, AdvertDetailDto>();
            CreateMap<PagingResultDto<Advert>, PagingResultDto<AdvertDto>>();
        }
    }
}
