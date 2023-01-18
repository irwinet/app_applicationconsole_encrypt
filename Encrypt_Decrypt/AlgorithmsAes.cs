using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt_Decrypt
{
    public static class AlgorithmsAes
    {
        #region "Symmetric"        
        public static void SymmetricEncryptionAndDecryption(string raw)
        {
            try
            {
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string    
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    // Print encrypted string    
                    Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                    // Decrypt the bytes to a string.    
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    // Print decrypted string. It should be same as raw data    
                    Console.WriteLine($"Decrypted data: {decrypted}");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        #endregion

        #region "Asymmetric"
        public static void EncryptAsymmetric(string raw)
        {
            //Initialize the byte arrays to the public key information.
            byte[] modulus =
            {
                214,46,220,83,160,73,40,39,201,155,19,202,3,11,191,178,56,
                74,90,36,248,103,18,144,170,163,145,87,54,61,34,220,222,
                207,137,149,173,14,92,120,206,222,158,28,40,24,30,16,175,
                108,128,35,230,118,40,121,113,125,216,130,11,24,90,48,194,
                240,105,44,76,34,57,249,228,125,80,38,9,136,29,117,207,139,
                168,181,85,137,126,10,126,242,120,247,121,8,100,12,201,171,
                38,226,193,180,190,117,177,87,143,242,213,11,44,180,113,93,
                106,99,179,68,175,211,164,116,64,148,226,254,172,147
            };

            byte[] exponent = { 1, 0, 1 };

            //Create values to store encrypted symmetric keys.
            byte[] encryptedSymmetricKey;
            byte[] encryptedSymmetricIV;

            //Create a new instance of the RSA class.
            RSA rsa = RSA.Create();

            //Create a new instance of the RSAParameters structure.
            RSAParameters rsaKeyInfo = new RSAParameters();

            //Set rsaKeyInfo to the public key values.
            rsaKeyInfo.Modulus = modulus;
            rsaKeyInfo.Exponent = exponent;

            //Import key parameters into rsa.
            rsa.ImportParameters(rsaKeyInfo);

            //Create a new instance of the default Aes implementation class.
            Aes aes = Aes.Create();

            //Encrypt the symmetric key and IV.
            encryptedSymmetricKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
            encryptedSymmetricIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.Pkcs1);            
        }
        #endregion
    }
}
