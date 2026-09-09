using Microsoft.AspNetCore.Mvc;

namespace soclean.presentation.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
