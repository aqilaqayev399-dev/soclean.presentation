using AutoMapper;
using soclean.business.Dtos.Contact;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class ContactService : CrudService<Contact, ContactCreateDto, ContactUpdateDto, ContactDto>, IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IEmailService _emailService;
    public ContactService(IContactRepository repository, IMapper mapper, IEmailService emailService) : base(repository, mapper)
    {
        _contactRepository = repository;
        _emailService = emailService;
    }


    public async Task ContactCreateAsync(ContactCreateDto contactDto)
    {
        if (contactDto == null)
            throw new ArgumentNullException(nameof(contactDto));


        var model = new Contact
        {
            Email = contactDto.Email,
            Message = contactDto.Message,
            PhoneNumber = contactDto.PhoneNumber,
            Name = contactDto.Name,
            IsAnswer = false

        };

        await _contactRepository.CreateAsync(model);
    }

    public async Task<ContactCreateDto> ContactCreateVMAsync(int id)
    {
        var model = await _contactRepository.GetAsync(id);

        if (model == null)
        {
            throw new NotFoundException();
        }

        var vm = new ContactCreateDto { Name = model.Name, Email = model.Email, Message = model.Message, Id = model.Id, PhoneNumber = model.PhoneNumber };

        return vm;
    }

    public async Task<bool> SendEmailContact(ContactCreateDto vm)
    {
        if (vm == null)
        {
            throw new NotFoundException();
        }



        _emailService.SendEmail(vm.Email, "Dear Customer", vm.Answer);

        var model = await _contactRepository.GetAsync(vm.Id);
        if (model == null)
        {
            throw new NotFoundException();
        }

        model.IsAnswer = true;

        _contactRepository.Update(model);
        await _contactRepository.SaveChangesAsync();

        return true;
    }
}
