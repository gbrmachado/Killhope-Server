using System;
using System.Security.Cryptography;

namespace Killhope.Plugins.Manager.Domain.Hashing
{
    public class SHA1HashProvider : IHashProvider
    {
        public string ComputeHash(byte[] data)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                return Convert.ToBase64String(sha1.ComputeHash(data));
        }
    }
}
