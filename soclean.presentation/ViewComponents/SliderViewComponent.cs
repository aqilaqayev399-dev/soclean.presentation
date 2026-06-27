using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;

namespace soclean.presentation.ViewComponents;

public class SliderViewComponent : ViewComponent
{
    private readonly ISliderService _sliderService;

    public SliderViewComponent(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = await _sliderService.GetAllAsync();

        return View(vm);

    }
}

