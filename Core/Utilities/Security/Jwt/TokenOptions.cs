using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; } //Token'ın kullanıcı kitlesi
        public string Issuer { get; set; } //İmzalayan
        public int AccessTokenExpiration { get; set; }//Token son geçerlilik tarihi (dakika olarak)  .
        public string SecurityKey { get; set; }
        //public string RefreshSecurityKey { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
