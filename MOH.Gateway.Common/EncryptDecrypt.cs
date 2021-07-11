using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MOH.Gateway.Common
{
    public class EncryptDecrypt
    {
        public static string Encrypt(string clearText)
        {
            if (clearText == null) return clearText;
            string EncryptionKey = "inTErtECpaWENkey";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                        cryptoStream.Close();
                    }
                    clearText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                // string EncryptionKey = "MAKV2SPBNI99212";
                string EncryptionKey = "inTErtECpaWENkey";
                //string EncryptionKey = "8080808080808080";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                            cryptoStream.Close();
                        }
                        cipherText = System.Text.Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
