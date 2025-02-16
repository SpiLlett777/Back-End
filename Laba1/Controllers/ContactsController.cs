using Microsoft.AspNetCore.Mvc;

namespace Laba1.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
