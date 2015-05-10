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

        public static Hash CreateFrom(params object[] compositeItems)
        {
            var compositeKey = String.Join(" ", compositeItems.ToString());
            var messageBytes = Encoding.Default.GetBytes(compositeKey);
            var sha512 = new SHA512Managed().ComputeHash(messageBytes);

            return new Hash(sha512);
        }

        public static Hash CreateNew()
        {
            return CreateFrom(Guid.NewGuid().ToString());
        }

        public static Hash Parse(string hashString)
        {
            return new Hash(Convert.FromBase64String(hashString));
        }

        public override string ToString()
        {
            return Convert.ToBase64String(_key);
        }

        public static bool operator ==(Hash a, Hash b)
        {
            return a._key.SequenceEqual(b._key);
        }

        public static bool operator !=(Hash a, Hash b)
        {
            return !a._key.SequenceEqual(b._key);
        }

        public static bool operator ==(Hash a, string b)
        {
            return a.ToString() == b;
        }

        public static bool operator !=(Hash a, string b)
        {
            return a.ToString() != b;
        }

        public static bool operator ==(string a, Hash b)
        {
            return a.ToString() == b;
        }

        public static bool operator !=(string a, Hash b)
        {
            return a.ToString() != b;
        }

        public override int GetHashCode()
        {
            return BitConverter.ToInt32(_key, 32);
        }

        public class Comparer : EqualityComparer<Hash>
        {
            public override bool Equals(Hash x, Hash y)
            {
                return x == y;
            }

            public override int GetHashCode(Hash obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
