using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations;

namespace soclean.presentation.ViewComponents;

public class PopularProductViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public PopularProductViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = await _productService.GetAllAsync();

        return View(vm);

    }
}
