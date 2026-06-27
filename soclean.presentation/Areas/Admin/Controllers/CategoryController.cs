using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Category;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Areas.Admin.Controllers;

[Area("Admin")]

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = categories;

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View(dto);
        }

        try
        {
            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;

            return View(dto);
        }

    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _categoryService.GetUpdateDtoAsync(id);
        var categories = await _categoryService.GetCategoriesForEditAsync(id);

        ViewBag.Categories = categories;

        return View(dto);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetCategoriesForEditAsync(dto.Id);
            ViewBag.Categories = categories;

            return View(dto);
        }

        try
        {
            await _categoryService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);

            var categories = await _categoryService.GetCategoriesForEditAsync(dto.Id);
            ViewBag.Categories = categories;

            return View(dto);
        }
    }
}

