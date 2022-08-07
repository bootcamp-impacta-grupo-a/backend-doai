using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoaiApi.Services
{
    public static class CryptService
    {
        private static readonly string SenhaInterna = "ac4d49311a2748a3be452d2b0d1736b9";
        private static readonly byte[] IVByteInterno = new byte[16]
    {
       253,
       79,
       128,
       98,
       78,
       88,
       89,
       128,
       67,
       137,
       230,
       137,
       113,
       37,
       91,
       30
    };

        public static string EncryptString_Aes(string texto)
        {
            byte[] Key = Encoding.ASCII.GetBytes(SenhaInterna);

            if (texto == null || texto.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IVByteInterno == null || IVByteInterno.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IVByteInterno;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptString_Aes(string base64)
        {
            byte[] Key = Encoding.ASCII.GetBytes(SenhaInterna);
            var cipherText = Convert.FromBase64String(base64);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IVByteInterno == null || IVByteInterno.Length <= 0)
                throw new ArgumentNullException("IV");

            string texto = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IVByteInterno;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            texto = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return texto;
        }

        public static string GetHash(string input)
        {
            SHA512 sha512Hash = SHA512.Create();
            byte[] data = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
