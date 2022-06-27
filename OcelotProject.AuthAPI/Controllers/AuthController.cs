using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OcelotProject.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper;

        public AuthController(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            var roles = new List<string>() { "admin" };
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, roles[0])
            };
            var user = new IdentityUser() { Id = "1", Email = "srhtayata@gmail.com", NormalizedEmail = "SRHTAYATA@GMAIL.COM", UserName = "srht", SecurityStamp = "serhat", NormalizedUserName = "SRHT", PhoneNumber = "5555555" };
            var accessToken = _tokenHelper.CreateToken(user, claims);
            var resultRefreshToken = _tokenHelper.CreateRefreshToken(user, claims);
            accessToken.RefreshToken = resultRefreshToken.Token;
            return StatusCode(StatusCodes.Status200OK, accessToken);
        }
        #endregion

    }
}
