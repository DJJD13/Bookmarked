using Bookmarked.Server.Dtos.Account;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<AppUser> _singInManager = signInManager;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

                if (user == null) return Unauthorized("Username or password is invalid. Please try again");

                var result = await _singInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized("Username or password is invalid. Please try again");

                return Ok(
                    new NewUserDto
                    {
                        UserName = user.UserName!,
                        Email = user.Email!,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

                if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                return roleResult.Succeeded ? Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName!, 
                        Email = appUser.Email!, 
                        Token = _tokenService.CreateToken(appUser)
                    }) : StatusCode(500, roleResult.Errors);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        
    }
}
