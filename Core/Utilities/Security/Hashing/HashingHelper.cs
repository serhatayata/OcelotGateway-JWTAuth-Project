using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static string CreatedKeyHash(string publicKey, string secretKey)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(GetHashFromString(secretKey));
            return GetStringFromHash(hmac.ComputeHash(GetHashFromString(publicKey)));
        }

        public static bool VerifyKeyHash(string publicKey, string secretKey, string hash)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(GetHashFromString(secretKey));
            var computedHash = GetStringFromHash(hmac.ComputeHash(GetHashFromString(publicKey)));
            return hash == computedHash;
        }

        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = GetStringFromHash(hmac.Key);
                passwordHash = GetStringFromHash(hmac.ComputeHash(GetHashFromString(password)));
            }
        }

        public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            byte[] pwHash = GetHashFromString(passwordHash);
            using (var hmac = new System.Security.Cryptography.HMACSHA512(GetHashFromString(passwordSalt)))
            {
                var computedHash = hmac.ComputeHash(GetHashFromString(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != pwHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        public static byte[] GetHashFromString(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
