using AutoMapper;
using soclean.business.Dtos.Partner;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class PartnerMapperProfile : Profile
{
    public PartnerMapperProfile()
    {
        CreateMap<Partner, PartnerDto>().ReverseMap();
        CreateMap<Partner, PartnerCreateDto>().ReverseMap();
        CreateMap<Partner, PartnerUpdateDto>().ReverseMap();
    }
}
