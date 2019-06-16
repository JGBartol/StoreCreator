using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using StoreCreator;

public class CryptoMethods
{

    public static string RandomNumber()
    {
        byte[] RandomValue = new byte[16];
        RandomNumberGenerator gen = RandomNumberGenerator.Create();
        gen.GetBytes(RandomValue);

        return Convert.ToBase64String(RandomValue);
    }

    public static string RandomNumber(int Size)
    {
        byte[] RandomValue = new byte[Size];
        RandomNumberGenerator gen = RandomNumberGenerator.Create();
        gen.GetBytes(RandomValue);

        return Convert.ToBase64String(RandomValue);

    }


    #region SymmetriAlgorithms

    public class SymmetricAlgorithms
    {
        private static bool _ProtectKey;
        private static string _AlgorithmName;

        public static bool ProtectKey
        {
            get { return _ProtectKey; }
            set { _ProtectKey = value; }
        }
        public static string AlgorithmName
        {
            get { return _AlgorithmName; }
            set { _AlgorithmName = value; }
        }

        public static void GenerateKey(string TargetFile)
        {
            SymmetricAlgorithm alg = SymmetricAlgorithm.Create(AlgorithmName);
            alg.GenerateKey();

            byte[] key = alg.Key;

            if (ProtectKey)
            {
                key = ProtectedData.Protect(key, null, DataProtectionScope.LocalMachine);

            }

            using (FileStream fs = new FileStream(TargetFile, FileMode.Create))
            {
                fs.Write(key, 0, key.Length);
            }
        }

        public static void ReadKey(SymmetricAlgorithm alg, string keyFile)
        {
            byte[] key;

            using (FileStream fs = new FileStream(keyFile, FileMode.Open))
            {
                key = new byte[fs.Length];
                fs.Read(key, 0, (int)fs.Length);

            }

            if (ProtectKey)
            {
                alg.Key = ProtectedData.Unprotect(key, null, DataProtectionScope.LocalMachine);
            }
            else
            {
                alg.Key = key;
            }

        }

        public static byte[] EncryptData(string data, string keyFile)
        {
            byte[] ClearData = System.Text.Encoding.UTF8.GetBytes(data);

            SymmetricAlgorithm alg = SymmetricAlgorithm.Create(AlgorithmName);
            ReadKey(alg, keyFile);
            MemoryStream ms = new MemoryStream();

            alg.GenerateIV();
            ms.Write(alg.IV, 0, alg.IV.Length);

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(ClearData, 0, ClearData.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public static string DecryptData(byte[] data, string keyFile)
        {
            SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
            ReadKey(Algorithm, keyFile);

            // Decrypt information
            MemoryStream Target = new MemoryStream();

            // Read IV
            int ReadPos = 0;
            byte[] IV = new byte[Algorithm.IV.Length];
            Array.Copy(data, IV, IV.Length);
            Algorithm.IV = IV;
            ReadPos += Algorithm.IV.Length;

            CryptoStream cs = new CryptoStream(Target, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, ReadPos, data.Length - ReadPos);
            cs.FlushFinalBlock();

            // Get the bytes from the memory stream and convert them to text
            return Encoding.UTF8.GetString(Target.ToArray());

        }
    }

    #endregion


    public class AsymmetricAlgorithms
    {
        public static string GenerateKey(string targetFile)
        {
            RSACryptoServiceProvider alg = new RSACryptoServiceProvider();

            string CompleteKey = alg.ToXmlString(true);
            byte[] KeyBytes = Encoding.UTF8.GetBytes(CompleteKey);

            KeyBytes = ProtectedData.Protect(KeyBytes, null, DataProtectionScope.LocalMachine);

            using (FileStream fs = new FileStream(targetFile, FileMode.Create))
            {
                fs.Write(KeyBytes, 0, KeyBytes.Length);
            }

            return alg.ToXmlString(false);
        }

        private static void ReadKey(RSACryptoServiceProvider alg, string keyfile)
        {
            byte[] keybytes;

            using (FileStream fs = new FileStream(keyfile, FileMode.Open))
            {
                keybytes = new byte[fs.Length];
                fs.Read(keybytes, 0, (int)fs.Length);
            }

            keybytes = ProtectedData.Unprotect(keybytes, null, DataProtectionScope.LocalMachine);

            alg.FromXmlString(Encoding.UTF8.GetString(keybytes));
        }

        public static byte[] EncryptData(string data, string publicKey)
        {
            RSACryptoServiceProvider alg = new RSACryptoServiceProvider();
            alg.FromXmlString(publicKey);

            return alg.Encrypt(Encoding.UTF8.GetBytes(data), true);

        }

        public static string DecryptData(byte[] data, string keyFile)
        {
            RSACryptoServiceProvider alg = new RSACryptoServiceProvider();
            ReadKey(alg, keyFile);

            byte[] clear = alg.Decrypt(data, true);
            return Convert.ToString(Encoding.UTF8.GetString(clear));

        }
    }

}




