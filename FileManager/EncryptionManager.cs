using System;

namespace FileManager
{
    public class EncryptionManager
    {
        public byte[] CryptXOR(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= 1;
            return bytes;
        }
    }
}
