using AutoMapper;
using soclean.business.Dtos.Subscribe;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class SubscribeMapperProfile : Profile
{
    public SubscribeMapperProfile()
    {
        CreateMap<Subscribe, SubscribeDto>().ReverseMap();
        CreateMap<Subscribe, SubscribeCreateDto>().ReverseMap();
        CreateMap<Subscribe, SubscribeUpdateDto>().ReverseMap();
    }
}
