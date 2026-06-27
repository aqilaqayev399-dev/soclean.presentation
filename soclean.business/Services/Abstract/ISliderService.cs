using soclean.business.Dtos.Slider;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface ISliderService : ICrudService<Slider, SliderCreateDto, SliderUpdateDto, SliderDto>
{
    Task CreateAsync(SliderCreateDto vm);
    Task<SliderUpdateDto> GetSliderUpdateDto(int id);
    Task UpdateSliderAsync(SliderUpdateDto vm);
}
