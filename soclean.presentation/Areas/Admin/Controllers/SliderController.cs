using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Slider;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Areas.Admin.Controllers;

[Area("Admin")]

public class SliderController : Controller
{
    private readonly ISliderService _sliderService;

    public SliderController(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetAllAsync();
        return View(sliders);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SliderCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _sliderService.CreateAsync(dto);
        return RedirectToAction("index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var slider = await _sliderService.GetSliderUpdateDto(id);
        return View(slider);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SliderUpdateDto dto)
    {
        if (dto == null)
        {
            return View();
        }
        await _sliderService.UpdateSliderAsync(dto);

        return RedirectToAction("index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _sliderService.DeleteAsync(id);
        return RedirectToAction("index");

    }
}