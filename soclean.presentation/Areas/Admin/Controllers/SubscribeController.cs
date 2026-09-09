using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SubscribeController : Controller
{
    private readonly ISubscribeService _subscribeService;

    public SubscribeController(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    public async Task<IActionResult> Index()
    {
        var subscribe = await _subscribeService.GetAllAsync();
        return View(subscribe);
    }
}
