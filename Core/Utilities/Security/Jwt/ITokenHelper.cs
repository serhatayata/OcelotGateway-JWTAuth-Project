using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Core.Utilities.Security.Jwt//Jwt : Json Web Token
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(IdentityUser user, List<Claim> rolesForClaims);
        RefreshToken CreateRefreshToken(IdentityUser user,List<Claim> claims);
    }
}