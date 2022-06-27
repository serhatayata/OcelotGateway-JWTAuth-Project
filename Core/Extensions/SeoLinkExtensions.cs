using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class SeoLinkExtensions
    {
        public static string GenerateSlug(string p1,string p2="")
        {
            string phrase = "";
            string middlePoint = "-";
            if (p2 == "" || p2 == null)
            {
                phrase = p1;
                middlePoint = "";
            }
            phrase = string.Format("{0}{1}{2}", p1,middlePoint ,p2);
              
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = str.Replace('ı', 'i');
            str = str.Replace('ö', 'o');
            str = str.Replace('ü', 'u');
            str = str.Replace('ş', 's');
            str = str.Replace('ğ', 'g');
            str = str.Replace('ç', 'c');
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
