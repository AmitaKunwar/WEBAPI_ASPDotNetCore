using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public UserProfileController(UserManager<ApplicationUser> _uManager)
        {
            _userManager = _uManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<Object> GetProfile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
            }

            var emailClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(emailClaim.Value);
            return new
            {
                user.Id,
                user.UserName,
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Birthdate,
                user.Gender,
            };
        }
    }
}
