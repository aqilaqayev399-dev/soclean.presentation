using AutoMapper;
using soclean.business.Dtos.Advertisement;
using soclean.business.Dtos.Slider;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class AdvertisementService : CrudService<Advertisement, AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementDto>, IAdvertisementService
{
    private readonly ICloudManager _cloudinaryManager;
    private readonly IAdvertisementRepository _advertisementRepository;

    public AdvertisementService(
     ICloudManager cloudinaryManager,
     IAdvertisementRepository repository,
     IMapper mapper)
     : base(repository, mapper)
    {
        _cloudinaryManager = cloudinaryManager;
        _advertisementRepository = repository;
    }

    public async Task CreateAsync(AdvertisementCreateDto vm)
    {


        var image = await _cloudinaryManager.FileCreateAsync(vm.PhotoFile);

        var advertisement = new Advertisement
        {
            Title = vm.Title,
         
            Photo = image

        };

        await _advertisementRepository.CreateAsync(advertisement);

    }




}
