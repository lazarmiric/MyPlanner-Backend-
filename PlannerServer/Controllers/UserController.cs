using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlannerServer.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlannerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSetings;

        public UserController(UserManager<User> userManager,SignInManager<User>signInManager,IOptions<ApplicationSettings> appSettings )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSetings = appSettings.Value;

        }

        [HttpPost]
        [Route("Register")]
        //POST: /User/Register
        public async Task<Object> PostUser(RegisterModel regUser)
        {
            var user = new User
            {
                UserName = regUser.UserName,
                FirstName = regUser.FirstName,
                LastName = regUser.LastName,
                Age = regUser.Age
            };
            try
            {
                var result = await _userManager.CreateAsync(user, regUser.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("Login")]
        //Post :/User/Login

        public async Task<IActionResult> Login(LoginModel lm)
        {
            var user = await _userManager.FindByNameAsync(lm.UserName);
            if(user != null && await _userManager.CheckPasswordAsync(user,lm.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
                //return token

            }
            else
            {
                return BadRequest( new {message = "Username or password is incorrect"}) ;
            }
              
        }
    }
}
