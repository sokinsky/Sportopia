using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STA {
    public class Encryption {
        #region Methods
        protected static string Salt {
            get {
                return "11a1aff3-2f9e-4848-89e4-56751f8e94a8";
            }
        }
        protected static string Password {
            get {
                return "4454637a-8503-4da2-b4fe-243d9a20af25";
            }
        }
        public static string Encrypt(string value) {
            string result = null;
            // generate the key from the shared secret and the salt

            byte[] salt = Encoding.ASCII.GetBytes(Salt);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Password, salt);

            // Create a RijndaelManaged object
            RijndaelManaged aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            // Create a decryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream()) {
                // prepend the IV
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                        //Write all data to the stream.
                        swEncrypt.Write(value);
                    }
                }
                result = Convert.ToBase64String(msEncrypt.ToArray());
            }
            return result;
        }
        public static string Decrypt(string value) {
            if (value == null)
                return "";
            byte[] salt = Encoding.ASCII.GetBytes(Salt);
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string result = null;

            try {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Password, salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(value);
                using (MemoryStream msDecrypt = new MemoryStream(bytes)) {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            result = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }
            return result;
        }
        private static byte[] ReadByteArray(Stream s) {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length) {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length) {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        public static string Hash(string value) {
            byte[] result;
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] temp = ue.GetBytes(value);

            SHA1Managed hash = new SHA1Managed();
            result = hash.ComputeHash(temp);
            return Convert.ToBase64String(result);
            //return System.Text.Encoding.UTF8.GetString(result);
        }
        #endregion
    }
}
