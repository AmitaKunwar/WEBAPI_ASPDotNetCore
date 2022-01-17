using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicatiomSetting _applicatiomSetting;

        public ApplicationUserController(UserManager<ApplicationUser> userMgr , SignInManager<ApplicationUser> signinMgr, IOptions<ApplicatiomSetting> appSetting)
        {
            _userManager = userMgr;
            _signInManager = signinMgr;
            _applicatiomSetting = appSetting.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> RegisterUser(ApplicationUserModel _apmodel)
        {
            var _user = new ApplicationUser()
            {
                UserName = _apmodel.UserName,
                Firstname = _apmodel.FirstName,
                Lastname = _apmodel.LastName,
                Birthdate = _apmodel.Birthdate,
                Gender = _apmodel.Gender,
                Email = _apmodel.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(_user, _apmodel.Password);
                return Ok(result);
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            //var jwtSettings = _configuration.GetSection("JwtSettings");
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, loginUser.Email),
                    }),
                    Issuer = _applicatiomSetting.validIssuer,
                    Audience = _applicatiomSetting.validAudience,
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicatiomSetting.Secret)), SecurityAlgorithms.HmacSha256Signature),

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new{token});
            }
            return Unauthorized();
        }

    }
}
