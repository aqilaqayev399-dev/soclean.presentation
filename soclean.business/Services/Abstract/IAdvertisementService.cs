using soclean.business.Dtos.Advertisement;
using soclean.business.Dtos.Subscribe;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface IAdvertisementService : ICrudService<Advertisement, AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementDto>
{
    Task CreateAsync(AdvertisementCreateDto vm);
}

