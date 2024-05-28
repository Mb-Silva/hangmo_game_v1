namespace Hangmo.Server.Services.Interfaces
{
    public interface ICryptHelper
    {
        byte[] Crypt(string decryptValue);
        public string Decrypt(byte[] cryptValue);

    }
}
