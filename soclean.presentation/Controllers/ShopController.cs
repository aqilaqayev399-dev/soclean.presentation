using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soclean.business.Dtos.Product;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations;

namespace soclean.presentation.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ShopController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(
         string? Title,
         string? location,
         int page = 1)
    {
        int take = 5;

        var paginatedProducts = await _productService.GetFilteredPaginatedToursAsync(
            Title, page, take);
        var categories = await _categoryService.GetAllAsync();

        var vm = new ProductHomeDto
        {
            Products = paginatedProducts,

            Categories = categories
          
        };

        return View(vm);
    }



    public async Task<IActionResult> Detail(int id)
    {
        var product = await _productService.GetAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction("index");

    }
}
