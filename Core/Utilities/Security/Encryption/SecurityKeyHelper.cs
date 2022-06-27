using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Jwt;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
