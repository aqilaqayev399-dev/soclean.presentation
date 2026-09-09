using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;

namespace soclean.presentation.ViewComponents;

public class AdvertisementViewComponent : ViewComponent
{
    private readonly IAdvertisementService _advertisementService;

    public AdvertisementViewComponent(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var advertisements = await _advertisementService.GetAllAsync();

        return View(advertisements);
    }
}