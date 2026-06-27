using Microsoft.AspNetCore.Mvc;
using soclean.business.Dtos.Blog;
using soclean.business.Dtos.Partner;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations;
using System.Threading.Tasks;

namespace soclean.presentation.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService) 
    { 
        _blogService = blogService;
    }
    public async Task<IActionResult> Index()
    {
        var blogs = await _blogService.GetAllAsync();
        return View(blogs);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult>Create(BlogCreateDto dto)
    {
        if(!ModelState.IsValid)
        {
            return View(dto);
        }

        await _blogService.CreateAsync(dto);
        return RedirectToAction("index");

    }

    public async Task<IActionResult> Edit(int id)
    {
        var blog = await _blogService.GetBlogUpdateDto(id);
        return View(blog);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(BlogUpdateDto dto)
    {
        if (dto == null)
        {
            return View();
        }
        await _blogService.UpdateBlogAsync(dto);

        return RedirectToAction("index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _blogService.DeleteAsync(id);
        return RedirectToAction("index");

    }
}
