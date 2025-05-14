using Laba_15.Dtos;
using Laba_15.Interfaces;
using Laba_15.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laba_15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _tokenService;

        public AuthController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new AppUser
                {
                    UserName = dto.Username,
                    Email = dto.Email,
                };

                var createdUser = await _userManager.CreateAsync(user, dto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                        return Ok(_tokenService.CreateToken(user));
                    else
                        return StatusCode(500, roleResult.Errors);
                }
                else
                    return StatusCode(500, createdUser.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
                return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Username is not found and/or password is incorrect!");

            return Ok(_tokenService.CreateToken(user));
        }
    }
}
