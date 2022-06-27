using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt//Jwt : Json Web Token
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
