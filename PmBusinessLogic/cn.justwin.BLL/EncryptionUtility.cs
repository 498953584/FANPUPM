namespace cn.justwin.BLL
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionUtility
    {
        private static string IV_64 = "justwin0";
        private static string KEY_64 = "justwin0";

        public static string Decryption(string data)
        {
            byte[] buffer3;
            byte[] bytes = Encoding.ASCII.GetBytes(KEY_64);
            byte[] rgbIV = Encoding.ASCII.GetBytes(IV_64);
            try
            {
                buffer3 = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream(buffer3);
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(stream2);
            return reader.ReadToEnd();
        }

        public static string Encryption(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(KEY_64);
            byte[] rgbIV = Encoding.ASCII.GetBytes(IV_64);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int keySize = provider.KeySize;
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(stream2);
            writer.Write(data);
            writer.Flush();
            stream2.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(stream.GetBuffer(), 0, (int) stream.Length);
        }
    }
}

