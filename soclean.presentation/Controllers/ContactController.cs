using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Contact;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        if (dto == null)
        {
            return View(dto);
        }
        var contact = await _contactService.CreateAsync(dto);
        return RedirectToAction("Index", "Home");
    }
}
