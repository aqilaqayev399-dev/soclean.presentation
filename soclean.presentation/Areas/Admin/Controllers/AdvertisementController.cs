using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Advertisement;
using soclean.business.Services.Abstract;
using System.Threading.Tasks;

namespace soclean.presentation.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdvertisementController : Controller
{
    private readonly IAdvertisementService _advertisementService;

    public AdvertisementController(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

    public async Task<IActionResult> Index()
    {
        var advertisements = await _advertisementService.GetAllAsync();
        return View(advertisements);
    }

    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdvertisementCreateDto vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        await _advertisementService.CreateAsync(vm);
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Edit(int id)
    {
        var advertisement = await _advertisementService.GetAsync(id);
        if (advertisement == null)
        {
            return NotFound();
        }
        return View(advertisement);
    }




    


}
