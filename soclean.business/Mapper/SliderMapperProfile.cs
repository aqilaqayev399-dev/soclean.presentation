using AutoMapper;
using soclean.business.Dtos.Slider;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class SliderMapperProfile : Profile
{
    public SliderMapperProfile()
    {
        CreateMap<Slider, SliderCreateDto>().ReverseMap();
        CreateMap<Slider, SliderDto>().ReverseMap();
        CreateMap<Slider, SliderUpdateDto>().ReverseMap();
    }
}
