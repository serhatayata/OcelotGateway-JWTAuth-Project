using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt.TokenValidators
{
    public class TokenValidator
    {
        private IConfiguration Configuration { get; }
        public TokenValidator(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public bool Validate(string refreshToken)
        {
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
