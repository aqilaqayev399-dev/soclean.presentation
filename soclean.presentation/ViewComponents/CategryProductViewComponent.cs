using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.CategoryProductVM;
using soclean.business.Services.Abstract;

namespace soclean.presentation.ViewComponents;

public class CategryProductViewComponent : ViewComponent
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CategryProductViewComponent(
        IProductService productService,
        ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        var products = await _productService.GetAllAsync();

        var vm = categories
            .Select(category => new
            {
                Category = category,
                Product = products.FirstOrDefault(x => x.CategoryId == category.Id)
            })
            .Where(x => x.Product != null)
            .Select(x => new CategoryProductVM
            {
                CategoryId = x.Category.Id,
                CategoryName = x.Category.Name,
                ProductImage = x.Product.PictureFile
            })
            .ToList();

        return View(vm);
    }
}
