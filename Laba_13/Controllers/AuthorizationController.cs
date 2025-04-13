using Laba_13.Entities;
using Laba_13.Models;
using Laba_13.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laba_13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await _authService.RegisterAsync(request);

            if (user is null)
                return BadRequest("Username already exists!");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var response = await _authService.LoginAsync(request);

            if (response is null)
                return BadRequest("Invalid username and/or password!");

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }
        
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyEdpoint()
        {
            return Ok("You are admin!");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokensAsync(request);

            if (response is null || response.AccessToken is null || response.RefreshToken is null)
                return Unauthorized("InvalidRefreshToken");

            return Ok(response);
        }
    }
}
