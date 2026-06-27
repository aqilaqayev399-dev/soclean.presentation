using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Product;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {

        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = categories;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View(dto);
        }


         await _productService.ProductCreate(dto);
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _productService.GetProductForEditAsync(id);

        if (model == null)
            return NotFound();

        ViewBag.Categories = await _categoryService.GetAllAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(dto);
        }

        await _productService.UpdateProductAsync(dto);

        return RedirectToAction(nameof(Index));
    }
}
