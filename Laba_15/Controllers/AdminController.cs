using Microsoft.AspNetCore.Mvc;

namespace Laba_15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Welcome to the Admin page", "text/plain");
        }
    }
}
