using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HOSAPI.Utils
{
    public static class Misc
    {
        public static string MD5Encrypt(string str)
        {
            string strOutput = null;
            int i = 0;

            // Create New Crypto Service Provider Object
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            // Convert the original string to array of Bytes
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);

            // Compute the Hash, returns an array of Bytes
            byte[] bytHash = md5.ComputeHash(bytValue);
            md5.Clear();

            for (i = 0; i <= bytHash.Length - 1; i++)
            {
                //don't lose the leading 0
                strOutput += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return strOutput;
        }

    }
}
