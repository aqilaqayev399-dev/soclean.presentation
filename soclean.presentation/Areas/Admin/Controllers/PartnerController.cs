using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Partner;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Areas.Admin.Controllers;

[Area("Admin")]
public class PartnerController : Controller
{
    private readonly IPartnerService _partnerService;

    public PartnerController(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    public async Task<IActionResult> Index()
    {
        var partners = await _partnerService.GetAllAsync();

        return View(partners);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PartnerCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _partnerService.CreateAsync(dto);
        return RedirectToAction("index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var partner = await _partnerService.GetPartnerUpdateDto(id);
        return View(partner);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PartnerUpdateDto dto)
    {
        if (dto == null)
        {
            return View();
        }
        await _partnerService.UpdatePartnerAsync(dto);

        return RedirectToAction("index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _partnerService.DeleteAsync(id);
        return RedirectToAction("index");

    }

}
