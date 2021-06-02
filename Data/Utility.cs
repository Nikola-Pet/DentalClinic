using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental.Data
{
    public static class Utility
    {
        public static string Key = "s!mp1eq@y";

        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        //public static string Dencrypt(string encodeData)
        //{
        //    if (string.IsNullOrEmpty(encodeData))
        //    {
        //        return null;
        //    }

        //    var encodeBytes = Convert.FromBase64String(encodeData);
        //    var result = Encoding.UTF8.GetString(encodeBytes);
        //    string password = result.Substring(0, result.Length - Key.Length);

        //    return password;
        //}
    }
}
