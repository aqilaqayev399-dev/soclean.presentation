using soclean.business.Dtos.Subscribe;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface ISubscribeService : ICrudService<Subscribe, SubscribeCreateDto, SubscribeUpdateDto, SubscribeDto>
{
}

