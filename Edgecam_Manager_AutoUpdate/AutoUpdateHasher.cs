using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Edgecam_Manager_AutoUpdate
{
    internal enum HashType
    {
        MD5,
        SHA1,
        SHA512
    }

    internal static class AutoUpdateHasher
    {
        public static string HashFile(string FilePath, HashType Type)
        {
            switch (Type)
            {
                case HashType.MD5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(FilePath, FileMode.Open)));
                case HashType.SHA1:
                    return MakeHashString(SHA1.Create().ComputeHash(new FileStream(FilePath, FileMode.Open)));
                case HashType.SHA512:
                    return MakeHashString(SHA512.Create().ComputeHash(new FileStream(FilePath, FileMode.Open)));
                default: return "";
            }
        }

        private static string MakeHashString(byte[] hash)
        {
            StringBuilder s = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
                s.Append(b.ToString("X2").ToLower());

            return s.ToString();
        }
    }
}