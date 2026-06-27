using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;

namespace soclean.presentation.ViewComponents;

public class PartnerViewComponent : ViewComponent
{
    private readonly IPartnerService _partnerService;

    public PartnerViewComponent(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = await _partnerService.GetAllAsync();

        return View(vm);

    }
}

