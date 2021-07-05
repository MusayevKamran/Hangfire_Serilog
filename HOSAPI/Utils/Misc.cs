using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HOSAPI.Utils
{
    public static class Misc
    {
        public static string ComputeSHA512(string plainTextInput, string salt)
        {
            HashAlgorithm algorithm = new SHA512Managed();
            byte[] saltBytes = GetBytes(salt);
            byte[] plainTextInputBytes = GetBytes(plainTextInput);

            byte[] plainTextWithSaltBytes = plainTextInputBytes.Concat(saltBytes).ToArray();
            byte[] saltedSHA512Bytes = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(saltedSHA512Bytes);
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            try
            {
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            }
            #region <<< Error Handlings >>>
            catch (Exception ex)
            {
                // Runtime.LogError(ex);
            }
            #endregion
            return bytes;
        }

    }
}
