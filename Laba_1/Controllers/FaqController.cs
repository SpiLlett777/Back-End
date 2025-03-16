using Microsoft.AspNetCore.Mvc;

namespace Laba1.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
