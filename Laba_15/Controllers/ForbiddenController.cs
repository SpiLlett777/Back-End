using Microsoft.AspNetCore.Mvc;

namespace Laba_15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForbiddenController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return StatusCode(403, "Access Denied");
        }
    }
}
