using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Salem.Utils {
    public static class Crypto {
        /// <summary>
        /// Computes the SHA-256 hash for the specified string.
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <returns>A hexadecimal string representation of the SHA-256 hash.</returns>
        public static string Hash(string input) {
            using (var sha256 = SHA256.Create()) {
                var result = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Encrypts the input string with the AES (Advanced Encryption Standard) algorith with a 128-bit key.
        /// </summary>
        /// <param name="data">The input string to be encrypted</param>
        /// <param name="key">The 16-character-long key to encrypt based on.</param>
        /// <returns>A base64-encoded string containing the result of encryption</returns>
        public static string Encrypt(string data, string key) {
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var memoryStream = new MemoryStream()) {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var streamWriter = new StreamWriter(cryptoStream))
                        streamWriter.Write(data);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Dencrypts the input string with the AES (Advanced Encryption Standard) algorith with a 128-bit key.
        /// </summary>
        /// <param name="data">The input string to be decrypted</param>
        /// <param name="key">The 16-character-long key to decrypt based on.</param>
        /// <returns>A string containing the result of decryption</returns>
        public static string Decrypt(string data, string key) {
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var memoryStream = new MemoryStream(Convert.FromBase64String(data)))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                    return streamReader.ReadToEnd();
            }
        }
    }
}
