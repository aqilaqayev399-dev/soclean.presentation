using AutoMapper;
using soclean.business.Dtos.Advertisement;
using soclean.business.Dtos.Product;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
    }
}

public class AdvertisementMapperProfile : Profile
{
    public AdvertisementMapperProfile()
    {
        CreateMap<Advertisement, AdvertisementDto>().ReverseMap();
        CreateMap<Advertisement, AdvertisementCreateDto>().ReverseMap();
        CreateMap<Advertisement, AdvertisementUpdateDto>().ReverseMap();
    }
}