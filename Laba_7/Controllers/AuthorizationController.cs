using Laba_7.Entities;
using Laba_7.Models;
using Laba_7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laba_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IAuthService authService, ILogger<AuthorizationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            _logger.LogInformation("Метод Register вызван для пользователя {Username}", request.Username);
            
            var user = await _authService.RegisterAsync(request);
            if (user is null)
            {
                _logger.LogWarning("Попытка регистрации неуспешна. Пользователь с именем {Username} уже существует", request.Username);
            
                return BadRequest("Username already exists!");
            }

            _logger.LogInformation("Пользователь {Username} успешно зарегистрирован", request.Username);
           
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            _logger.LogDebug("Метод Login вызван для пользователя {Username}", request.Username);
            var response = await _authService.LoginAsync(request);
            if (response is null)
            {
                _logger.LogWarning("Неуспешная попытка входа пользователя {Username}. Неверное имя или пароль", request.Username);
            
                return BadRequest("Invalid username and/or password!");
            }

            _logger.LogInformation("Пользователь {Username} успешно вошёл в систему", request.Username);
            
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            _logger.LogDebug("Запрошен защищённый эндпоинт для аутентифицированного пользователя");
            
            return Ok("You are authenticated!");
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            _logger.LogDebug("Запрошен эндпоинт администратора");
            
            return Ok("You are admin!");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            _logger.LogDebug("Метод RefreshToken вызван для пользователя с ID: {UserId}", request.UserId);

            var response = await _authService.RefreshTokensAsync(request);
            if (response is null || response.AccessToken is null || response.RefreshToken is null)
            {
                _logger.LogError("Ошибка при обновлении токенов для пользователя с ID: {UserId}", request.UserId);
                
                return Unauthorized("InvalidRefreshToken");
            }

            _logger.LogInformation("Токены для пользователя с ID: {UserId} успешно обновлены", request.UserId);
            
            return Ok(response);
        }
    }
}
