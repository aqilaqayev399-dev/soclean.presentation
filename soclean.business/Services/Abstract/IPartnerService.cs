using soclean.business.Dtos.Partner;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface IPartnerService : ICrudService<Partner, PartnerCreateDto, PartnerUpdateDto, PartnerDto>
{
    Task CreateAsync(PartnerCreateDto vm);
    Task UpdatePartnerAsync(PartnerUpdateDto vm);
    Task<PartnerUpdateDto> GetPartnerUpdateDto(int id);
}
