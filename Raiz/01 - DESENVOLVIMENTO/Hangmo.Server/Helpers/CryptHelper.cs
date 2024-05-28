using CSChaCha20;
using Hangmo.Server.Services.Interfaces;
using System.Text;

namespace Hangmo.Server.Helpers
{
    public class CryptHelper : ICryptHelper
    {
        byte[] key = Encoding.UTF8.GetBytes("ZKviVAs9HP0oGQPf3pxo40KDZxRC0b2r");
        byte[] iv = new byte[12];
        uint counter = 1;

        public byte[] Crypt(string decryptValue)
        {
            byte[] value = Encoding.ASCII.GetBytes(decryptValue);

            ChaCha20 forEncrypting = new ChaCha20(key, iv, counter);
            byte[] encryptedContent = new byte[value.Length];
            forEncrypting.EncryptBytes(encryptedContent, value);

            return encryptedContent;
        }

        public string Decrypt(byte[] cryptValue)
        {
            byte[] value = cryptValue;

            ChaCha20 forDecrypting = new ChaCha20(key, iv, counter);
            byte[] decryptedContent = new byte[value.Length];
            forDecrypting.DecryptBytes(decryptedContent, value);

            // Convertendo os bytes descriptografados para uma string
            string decryptedString = Encoding.ASCII.GetString(decryptedContent);

            return decryptedString;
        }
    }
}
