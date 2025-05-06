using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Laba9.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult HandleException()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Response.StatusCode = 500;
            ViewData["Code"] = 500;
            return View("StatusCode");
        }

        [HttpGet("/Error/{code:int}")]
        public IActionResult HandleStatusCode(int code)
        {
            Response.StatusCode = code;
            ViewData["Code"] = code;
            return View("StatusCode");
        }
    }
}