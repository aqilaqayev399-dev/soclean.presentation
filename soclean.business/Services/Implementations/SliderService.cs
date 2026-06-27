using AutoMapper;
using soclean.business.Dtos.Slider;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class SliderService : CrudService<Slider, SliderCreateDto, SliderUpdateDto, SliderDto>, ISliderService
{
    private readonly ISliderRepository _sliderRepository;
    private readonly ICloudinaryManager _cloudinaryManager;

    public SliderService(ISliderRepository repository, IMapper mapper, ICloudinaryManager cloudinaryManager) : base(repository, mapper)
    {
        _sliderRepository = repository;
        _cloudinaryManager = cloudinaryManager;
    }

    public async Task CreateAsync(SliderCreateDto vm)
    {


        var image = await _cloudinaryManager.FileCreateAsync(vm.PictureFile);

        var slider = new Slider
        {
            Title = vm.Description,
            Description = vm.Description,
            PictureFile = image

        };

        await _sliderRepository.CreateAsync(slider);

    }

    public async Task UpdateSliderAsync(SliderUpdateDto vm)
    {
        var slider = await _sliderRepository.GetAsync(vm.Id);
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

        slider.Title = vm.Title;
        slider.Description = vm.Description;

        _sliderRepository.Update(slider);
        await _sliderRepository.SaveChangesAsync();
    }



    public async Task<SliderUpdateDto> GetSliderUpdateDto(int id)
    {
        var sldier = await _sliderRepository.GetAsync(id);
        if (sldier == null)
        {
            throw new NotFoundException();
        }
        var update = new SliderUpdateDto
        {
            Id = sldier.Id,
            Title = sldier.Title,
            Description = sldier.Description,
            Picture = sldier.PictureFile,
        };

        return update;
    }
}
