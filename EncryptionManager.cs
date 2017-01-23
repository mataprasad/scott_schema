// ----------------------------------------------------------------------
// <copyright file="EncryptionManager.cs" company="KNockout Casino" >
//    Copyright statement. All right reserved
// </copyright>
// <summary>This is the Encryption Manager class</summary>
// ------------------------------------------------------------------------
namespace CdrParser.Util
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// This class contains diffrent Encryption Methods
    /// </summary>
    /// Created By Gaurav Yadav.
    /// StyleCop error and CodeAnalyser error removed by - Gaurav Yadav
    public static class EncryptionManager
    {
        /// <summary>
        ///  static declerations for this scope only
        /// </summary>
        /// <param name="textToBeDecrypted">this is decrypted text</param>
        /// <returns>it return th string</returns>
        public static string Decrypt(string textToBeDecrypted)
        {
            RijndaelManaged rijndaelCipher = null;
            const string Password = "TalgraceM";
            string decryptedData;
            MemoryStream memoryStream = null;
            PasswordDeriveBytes secretKey = null;
            try
            {
                byte[] encryptedData = Convert.FromBase64String(textToBeDecrypted);
                rijndaelCipher = new RijndaelManaged();
                byte[] salt = Encoding.ASCII.GetBytes(Password.Length.ToString(CultureInfo.InvariantCulture));
                ////Making of the key for decryption
                secretKey = new PasswordDeriveBytes(Password, salt);
                ////Creates a symmetric Rijndael decryptor object.
                ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));

                memoryStream = new MemoryStream(encryptedData);
                ////Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainText = new byte[encryptedData.Length];
                int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);

                ////Converting to string
                decryptedData = Encoding.UTF8.GetString(plainText, 0, decryptedCount);
                return decryptedData;
            }
            catch
            {
                decryptedData = textToBeDecrypted;
                throw;
            }
            finally
            {
                // cryptoStream.Close();
                if (rijndaelCipher != null)
                {
                    rijndaelCipher.Dispose();
                }

                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }

                if (secretKey != null)
                {
                    secretKey.Dispose();
                }
            }
        }

        /// <summary>
        /// Encryption method for the password
        /// </summary>
        /// <param name="textToBeEncrypted">text to encrypt</param>
        /// <returns>it return the string value</returns>
        public static string Encrypt(string textToBeEncrypted)
        {
            RijndaelManaged rijndaelCipher = null;
            PasswordDeriveBytes secretKey = null;
            try
            {
                rijndaelCipher = new RijndaelManaged();
                const string Password = "TalgraceM";
                byte[] plainText = System.Text.Encoding.UTF8.GetBytes(textToBeEncrypted);
                byte[] salt = Encoding.ASCII.GetBytes(Password.Length.ToString(CultureInfo.InvariantCulture));
                secretKey = new PasswordDeriveBytes(Password, salt);
                ////Creates a symmetric encryptor object. 
                ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ////Defines a stream that links data streams to cryptographic transformations
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    ////Writes the final state and clears the buffer
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherBytes = memoryStream.ToArray();

                    ////memoryStream.Close();
                    // cryptoStream.Close();
                    string encryptedData = Convert.ToBase64String(cipherBytes);

                    return encryptedData;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (rijndaelCipher != null)
                {
                    rijndaelCipher.Dispose();
                }

                if (secretKey != null)
                {
                    secretKey.Dispose();
                }
            }
        }

        /// <summary>
        /// This Method is used to Encrypt the data using MD5 Algorithm
        /// This method required for sending password to money booker
        /// </summary>
        /// <param name="textTobeEncrypted">What text we want to encrypt</param>
        /// <returns>it return the string value</returns>
        public static string Encrypt_MD5(string textTobeEncrypted)
        {
            MD5CryptoServiceProvider hasher = null;
            UTF8Encoding encoder = null;
            try
            {
                ////Instantiate MD5CryptoServiceProvider
                hasher = new MD5CryptoServiceProvider();

                encoder = new UTF8Encoding();

                ////convert a text into bytes using Getbyte() and compute hash (encoded password)
                byte[] hashedBytes = hasher.ComputeHash(encoder.GetBytes(textTobeEncrypted));

                ////Convert encoded bytes back to a 'readable' string 
                string encryptedData = BitConverter.ToString(hashedBytes);

                ////removing all "-" from string.
                encryptedData = encryptedData.Replace("-", string.Empty).Trim();

                ////returning a encrypted string.
                return encryptedData;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (hasher != null)
                {
                    hasher.Dispose();
                }

                encoder = null;
            }
        }
    }
}
