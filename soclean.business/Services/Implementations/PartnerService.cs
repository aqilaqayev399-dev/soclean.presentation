using AutoMapper;
using soclean.business.Dtos.Partner;
using soclean.business.Dtos.Slider;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class PartnerService : CrudService<Partner, PartnerCreateDto, PartnerUpdateDto, PartnerDto>, IPartnerService
{
    private readonly IPartnerRepository _partnerRepository;
    private readonly ICloudinaryManager _cloudinaryManager;


    public PartnerService(IPartnerRepository repository, IMapper mapper, ICloudinaryManager cloudinaryManager) : base(repository, mapper)
    {
        _partnerRepository = repository;
        _cloudinaryManager = cloudinaryManager;
    }

    public async Task CreateAsync(PartnerCreateDto vm)
    {


        var image = await _cloudinaryManager.FileCreateAsync(vm.PictureFile);

        var slider = new Partner
        {
            Name = vm.Name,
            Description = vm.Description,
            PictureFile = image

        };

        await _partnerRepository.CreateAsync(slider);

    }


    public async Task UpdatePartnerAsync(PartnerUpdateDto vm)
    {
        var slider = await _partnerRepository.GetAsync(vm.Id);
        if (slider == null)
        {
            throw new NotFoundException();
        }

        if (vm.PictureFile != null)
        {
            await _cloudinaryManager.FileDeleteAsync(slider.PictureFile);
            var image = await _cloudinaryManager.FileCreateAsync(vm.PictureFile);
            slider.PictureFile = image;

        }

        slider.Name = vm.Name;
        slider.Description = vm.Description;

        _partnerRepository.Update(slider);
        await _partnerRepository.SaveChangesAsync();
    }



    public async Task<PartnerUpdateDto> GetPartnerUpdateDto(int id)
    {
        var sldier = await _partnerRepository.GetAsync(id);
        if (sldier == null)
        {
            throw new NotFoundException();
        }
        var update = new PartnerUpdateDto
        {
            Id = sldier.Id,
            Name = sldier.Name,
            Description = sldier.Description,
            Picture = sldier.PictureFile,
        };

        return update;
    }




}
