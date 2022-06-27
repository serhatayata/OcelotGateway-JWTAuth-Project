using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Core.Extensions
{
    //Claim nesnesini genişlet, ek fonksiyonlar ekle.
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));
        }

        public static void AddPhoneNumber(this ICollection<Claim> claims, string phoneNumber)
        {
            claims.Add(new Claim(ClaimTypes.MobilePhone, phoneNumber));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role, role))); 
        }
        public static void AddUserData(this ICollection<Claim> claims, string userData)
        {
            claims.Add(new Claim(ClaimTypes.UserData, userData));
        }

        public static void AddIp(this ICollection<Claim> claims, string ip)
        {
            claims.Add(new Claim(ClaimTypes.Locality, ip));
        }
    }
}
