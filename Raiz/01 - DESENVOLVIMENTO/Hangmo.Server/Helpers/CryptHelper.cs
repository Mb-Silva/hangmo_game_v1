using CSChaCha20;
using Hangmo.Server.Services.Interfaces;
using System.Text;

namespace Hangmo.Server.Helpers
{
    public class CryptHelper 
    {
        private static byte[] key = Encoding.UTF8.GetBytes("ZKviVAs9HP0oGQPf3pxo40KDZxRC0b2r");
        private static byte[] iv = new byte[12];
        private static uint counter = 1;

        public static byte[] Crypt(string decryptValue)
        {
            byte[] value = Encoding.UTF8.GetBytes(decryptValue);

            ChaCha20 forEncrypting = new ChaCha20(key, iv, counter);
            byte[] encryptedContent = new byte[value.Length];
            forEncrypting.EncryptBytes(encryptedContent, value);

            return encryptedContent;
        }

        public static string Decrypt(byte[] cryptValue)
        {
            byte[] value = cryptValue;

            ChaCha20 forDecrypting = new ChaCha20(key, iv, counter);
            byte[] decryptedContent = new byte[value.Length];
            forDecrypting.DecryptBytes(decryptedContent, value);

            // Convertendo os bytes descriptografados para uma string
            string decryptedString = Encoding.UTF8.GetString(decryptedContent);
            return decryptedString;
        }
    }
}
