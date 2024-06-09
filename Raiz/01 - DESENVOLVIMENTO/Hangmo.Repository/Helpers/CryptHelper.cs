using CSChaCha20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Helpers
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
    }
}