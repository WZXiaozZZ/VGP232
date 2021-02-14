using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Assignment4
{
    public enum CryptoAlgorithm
    {
        RSA,
        AES
    }

    public class Crypto
    {
        private AesCryptoServiceProvider aes;
        private RSACryptoServiceProvider rsa;
        private CryptoAlgorithm mode;

        public void Initialize(CryptoAlgorithm m)
        {
            mode = m;
            if (m == CryptoAlgorithm.RSA)
            {
                rsa = new RSACryptoServiceProvider();
            }
            else
            {
                aes = new AesCryptoServiceProvider();
            }
        }

        public void SaveK1(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                File.WriteAllText(path, rsa.ToXmlString(false));
            }
            else
            {
                File.WriteAllBytes(path, aes.Key);
            }
        }

        public void SaveK2(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                File.WriteAllText(path, rsa.ToXmlString(true));
            }
            else
            {
                File.WriteAllBytes(path, aes.IV);
            }

        }

        public void LoadK1(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                rsa.FromXmlString(File.ReadAllText(path));
            }
            else
            {
                aes.Key = File.ReadAllBytes(path);
            }
        }

        public void LoadK2(string path)
        {
            if (mode == CryptoAlgorithm.RSA)
            {
                rsa.FromXmlString(File.ReadAllText(path));
            }
            else
            {
                aes.IV = File.ReadAllBytes(path);
            }
        }

        public byte[] Encrypt(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                return null;
            }
            if (mode == CryptoAlgorithm.RSA)
            {
                return rsa.Encrypt(input, false); 
            }
            else
            {
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write);
                csEncrypt.Write(input, 0, input.Length);
                csEncrypt.FlushFinalBlock();
                return msEncrypt.ToArray();
            }
        }

        public byte[] Decrypt(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                return null;
            }
            if (mode == CryptoAlgorithm.RSA)
            {
                return rsa.Decrypt(input, false);
            }
            else
            {
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateDecryptor(), CryptoStreamMode.Write);
                csEncrypt.Write(input, 0, input.Length);
                csEncrypt.FlushFinalBlock();
                return msEncrypt.ToArray();
            }
        }

    }
}
