using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
namespace JDownloaderDBDecrypt {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 0)
                Console.WriteLine("Error no input file");
            else {
                string output = Decrypt(args[0]);
                    using (var tw = new StreamWriter("output.txt", false))
                        tw.Write(output);
            }
        }

        private static string Decrypt(string filePath) {
            byte[] data = File.ReadAllBytes(filePath);
            sbyte[] skey = { 1, 6, 4, 5, 2, 7, 4, 3, 12, 61, 14, 75, -2, -7, -44, 33 };
            byte[] key = skey.Select(sb => unchecked((byte)sb)).ToArray();
            using (AesCryptoServiceProvider _acs = new AesCryptoServiceProvider()) {
                _acs.Key = key;
                _acs.IV = key;
                ICryptoTransform decryptor = _acs.CreateDecryptor(_acs.Key, _acs.IV);
                using (MemoryStream msDecrypt = new MemoryStream(data))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    return srDecrypt.ReadToEnd();
            }
        }
    }
}
