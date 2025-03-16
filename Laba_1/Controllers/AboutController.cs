using Microsoft.AspNetCore.Mvc;

namespace Laba1.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
