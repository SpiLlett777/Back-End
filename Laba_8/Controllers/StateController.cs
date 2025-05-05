using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laba8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        // Установить значение в сессию
        [HttpGet("/session/set/{value}")]
        public IActionResult SessionSet(string value)
        {
            HttpContext.Session.SetString("SessionKey", value);
            return Ok($"На серверную сессию записано значение: {value}");
        }

        // Получить значение из сессии
        [HttpGet("/session/get")]
        public IActionResult SessionGet()
        {
            var value = HttpContext.Session.GetString("SessionKey") ?? "<не задано>";
            return Ok($"С серверной сессии прочитано значение: {value}");
        }

        // Установить куки
        [HttpGet("/cookie/get/{value}")]
        public IActionResult CookieSet(string value)
        {
            Response.Cookies.Append("MyCookie", value, new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            });
            return Ok($"Cookie установлен: {value}");
        }

        // Прочитать куки
        [HttpGet("/cookie/set")]
        public IActionResult CookieSet()
        {
            if (Request.Cookies.TryGetValue("MyCookie", out var value))
                return Ok($"Cookie = {value}");

            return Ok($"Cookie не найден");
        }
    }
}
