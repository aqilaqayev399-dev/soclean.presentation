using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Subscribe;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Controllers;

public class SubscribeController : Controller
{
    private readonly ISubscribeService _subscribeService;

    public SubscribeController(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeCreateDto dto)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

        await _subscribeService.CreateAsync(dto);
        return RedirectToAction("Index", "Home");
    }
}