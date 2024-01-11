using Lesson1API.Models.DTOs;
using Lesson1API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1API.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }




        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
      public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                //Add roles tto this User
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    await userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles.ToString());
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }    
                }    
            }
            return BadRequest("Somthing went wrong");
        }


        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if(user != null)
            {
                var checkPasswork = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if(checkPasswork)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    // Create Token
                    var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                    var respone = new LoginRequestDto
                    {
                        JwtToken = jwtToken
                    };
                    
                    return Ok(respone);
                }    
            }    
            return BadRequest();
        }
    }
}
