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
    }
}
