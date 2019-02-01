using System;
using System.Text;
using System.Security.Cryptography;

namespace GttApiWeb.Helpers
{
    class Encrypt
    {
        public static string Hash(string value)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider mD5CryptoService = new MD5CryptoServiceProvider();
            byte[] bytes = mD5CryptoService.ComputeHash(new UTF8Encoding().GetBytes(value));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}

