using AutoMapper;
using soclean.business.Dtos.Contact;
using soclean.core.Entities;

namespace soclean.business.Mapper;

public class ContactMapperProfile : Profile
{
    public ContactMapperProfile()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Contact, ContactCreateDto>().ReverseMap();
        CreateMap<Contact, ContactUpdateDto>().ReverseMap();
    }

    protected internal ContactMapperProfile(string profileName) : base(profileName)
    {
    }
}
