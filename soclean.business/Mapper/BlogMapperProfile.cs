using AutoMapper;
using soclean.business.Dtos.Blog;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class BlogMapperProfile : Profile
{
    public BlogMapperProfile()
    {
        CreateMap<Blog, BlogDto>().ReverseMap();
        CreateMap<Blog, BlogCreateDto>().ReverseMap();
        CreateMap<Blog, BlogUpdateDto>().ReverseMap();
    }
}