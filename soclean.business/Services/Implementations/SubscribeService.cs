using AutoMapper;
using soclean.business.Dtos.Subscribe;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class SubscribeService : CrudService<Subscribe, SubscribeCreateDto, SubscribeUpdateDto, SubscribeDto>, ISubscribeService
{
    public SubscribeService(ISubscribeRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
