using soclean.business.Dtos.Contact;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface IContactService : ICrudService<Contact, ContactCreateDto, ContactUpdateDto, ContactDto>
{
    Task<ContactCreateDto> ContactCreateVMAsync(int id);
    Task<bool> SendEmailContact(ContactCreateDto vm);
}
