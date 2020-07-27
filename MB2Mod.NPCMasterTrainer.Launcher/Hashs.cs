using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using sha384 = System.Security.Cryptography.SHA384;

namespace MB2Mod.NPCMasterTrainer.Launcher
{
    internal static partial class Hashs
    {
        private static byte[] ComputeHash<T>(Stream inputStream, T hashAlgorithm) where T : HashAlgorithm
        {
            if (hashAlgorithm == null)
                throw new ArgumentNullException(nameof(hashAlgorithm));
            if (inputStream == null)
                throw new ArgumentOutOfRangeException(nameof(inputStream));
            byte[] result = hashAlgorithm.ComputeHash(inputStream);
            return result;
        }

        private static string ComputeHashString<T>(Stream inputStream, T hashAlgorithm, bool isLower = true) where T : HashAlgorithm
        {
            var temp = ComputeHash(inputStream, hashAlgorithm);
            return string.Join(null, temp.Select(x => x.ToString($"{(isLower ? "x" : "X")}2")));
        }

        private static sha384 CreateSHA384()
        {
            try
            {
                return sha384.Create();
            }
            catch
            {
                return new SHA384CryptoServiceProvider();
            }
        }

        public static string SHA384_String(Stream inputStream, bool isLower = true) => ComputeHashString(inputStream, CreateSHA384(), isLower);
    }
}