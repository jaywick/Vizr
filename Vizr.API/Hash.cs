using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class Hash
    {
        private byte[] _key;

        private Hash(byte[] key)
        {
            _key = key;
        }

        public static Hash FromExisting(byte[] data)
        {
            return new Hash(data);
        }

        public static Hash CreateFrom(string uniqueMessage)
        {
            var messageBytes = Encoding.Default.GetBytes(uniqueMessage);
            var sha512 = new SHA512Managed().ComputeHash(messageBytes);

            return new Hash(sha512);
        }

        public static Hash CreateNew()
        {
            return CreateFrom(Guid.NewGuid().ToString());
        }
    }
}
