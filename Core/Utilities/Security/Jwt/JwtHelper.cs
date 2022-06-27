using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        private DateTime _refreshTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        #region CreateToken
        public AccessToken CreateToken(IdentityUser user, List<Claim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };
        }
        #endregion
        #region CreateJwtSecurityToken
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, IdentityUser user,
            SigningCredentials signingCredentials, List<Claim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                //notBefore:DateTime.Now,//AccessTokenExpiration zamanı şimdiden önce ise token geçerli değil.
                claims: setClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );

            return jwt;
        }
        #endregion
        #region CreateRefreshToken
        public RefreshToken CreateRefreshToken(IdentityUser user, List<Claim> operationClaims)
        {
            _refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityRefreshToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new RefreshToken
            {
                Token = token,
                Expiration = _refreshTokenExpiration,
            };
        }
        #endregion
        #region CreateJwtSecurityRefreshToken
        public JwtSecurityToken CreateJwtSecurityRefreshToken(TokenOptions tokenOptions, IdentityUser user,
        SigningCredentials signingCredentials, List<Claim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _refreshTokenExpiration,
                claims: setClaims(user, operationClaims),
                //notBefore:DateTime.Now,//AccessTokenExpiration zamanı şimdiden önce ise token geçerli değil.
                signingCredentials: signingCredentials
            );

            return jwt;
        }
        #endregion
        #region SetClaims
        private IEnumerable<Claim> setClaims(IdentityUser user, List<Claim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id);
            claims.AddUserData(user.SecurityStamp);
            claims.AddPhoneNumber(user.PhoneNumber);
            claims.AddName($"{user.UserName} {user.NormalizedUserName}");
            //claims.AddName($"{user.FirstName} {user.LastName}");

            if (!string.IsNullOrEmpty(user.Email))
                claims.AddEmail(user.Email);

            var ip = operationClaims.Where(x => x.Type == ClaimTypes.Locality).Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(ip))
                claims.AddIp(operationClaims.Where(x => x.Type == ClaimTypes.Locality).Select(c => c.Value)
                     .FirstOrDefault());

            claims.AddRoles(operationClaims.Where(x => x.Type != ClaimTypes.Locality).Select(c => c.Value).ToArray());
            return claims;
        }
        #endregion

    }
}
