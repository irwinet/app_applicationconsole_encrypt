using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Encrypt_Decrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string _key = "abcdefgh";
            string privatekey = "hgfedcba";
            string message = "Hola Mundo";
            string encryption = AlgorithmsDes.SymmetricEncryption(message, _key, privatekey);
            string decryption = AlgorithmsDes.SymmetricDecryption(encryption, _key, privatekey);

            Console.WriteLine(string.Format("Encryption: {0} \nResult: {1}", message, encryption));
            Console.WriteLine("______________________________________________");
            Console.WriteLine(string.Format("\nDecryption: {0} \nResult: {1}", encryption, decryption));
            */

            /*
            string message = "Hola Mundo";
            AlgorithmsAes.SymmetricEncryptionAndDecryption(message);
            */

            /*
            string _key = "AQIDBAUGBwgJEBESExQVFg==";
            var encryptedText = SymmetricEncryptionDecryptionManager.Encrypt("Hola Mundo", _key);
            Console.WriteLine(encryptedText);
            var decryptedText = SymmetricEncryptionDecryptionManager.Decrypt(encryptedText, _key);
            Console.WriteLine(decryptedText);
            */

            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            var cipherText = AsymmetricEncryptionDecryptionManager.Encrypt("Hola Mundo", rsaCryptoServiceProvider.ExportParameters(false));
            Console.WriteLine(cipherText);
            var plainText = AsymmetricEncryptionDecryptionManager.Decrypt(cipherText, rsaCryptoServiceProvider.ExportParameters(true));
            Console.WriteLine(plainText);

            Console.ReadLine();
        }
    }
}
