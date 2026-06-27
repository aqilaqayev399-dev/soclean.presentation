using Microsoft.AspNetCore.Mvc;
using soclean.business.Services.Abstract;

namespace soclean.presentation.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task< IActionResult> Index()
    {
        var blogs =await  _blogService.GetAllAsync();
        return View(blogs);
    } 

    public async Task<IActionResult> Detail(int id)
    {
       var blog = await _blogService.GetAllBlogsAsync(id);
       return View(blog);
    }



}
