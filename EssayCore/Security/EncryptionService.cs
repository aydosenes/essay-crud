using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Essay.Core
{
    public class EncryptionService : IEncryptionService
    {
        public string _captchaSecurityKey = "mqUQS4BgSqA3acyKm2w8cTVj";
        private string _encryptionKey = "eApbkSpQJP2zHLaP2pJtY6QM";

        //
        //TsY5JJpZkcDhmD9ZpTQsVRLF
        //zne4W5VmEc9a7ZwbB6earZVV
        //at96Qu3aUvKpvss6ErbjAmVb
        //eApbkSpQJP2zHLaP2pJtY6QM
        //TajBZs6nHqpda79GCAMkmkFv
        //KLjkhX3yW57zbwnPLUuJjERz
        //t7g9VrxHrKYR8RK7UMnLUcUZ
        //Y3UrArVxCFeVUzJEP5Np4gnV
        //UQ98KJKdvuyxKw92hU4MVmup
        //ZQCRF8ePKxquXrhssGtTgN3r
        //mqUQS4BgSqA3acyKm2w8cTVj
        //gkDDmWKTLsaFesdXhZjKJyHq
        //e6Pcc5WEh9yphVTBXUPekSxh
        //HeULZnzL5TcH2RD9qkppHTX8
        //VtM9gZya2ncUhV2kvVcT3L73

        public virtual string PrivateKey(int id)
        {
            long result = (id * 2) + 5194;
            return result.ToString();
        }

        public virtual string CreatePasswordSHA512(string text)
        {
            Encoding encoding = Encoding.UTF8;
            SHA512 sha = new SHA512Managed();
            string HashedPassword = text;
            byte[] hashbytes = encoding.GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            StringBuilder s = new StringBuilder();
            int length = inputbytes.Length;
            for (int n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", inputbytes[n]).Replace(" ", "0"));
            }

            return s.ToString().ToUpper();
        }

        public virtual string CreatePasswordHash(string password, string privateKey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, privateKey);

            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        public string CreatePassword(int passwordLength)
        {
            var capitalLetters = "ABCDEFGHJKLMNOPRSTUVYZ"; // ignore: X,Q,W,I
            var smallLetters = "abcdefghjklmnoprstuvyz"; // ignore: X,Q,W,I
            string numbers = "0123456789";
            string allChars = capitalLetters + numbers + smallLetters;

            string key = "";

            var rnd = new Random();
            key += capitalLetters[rnd.Next(0, capitalLetters.Length - 1)].ToString();
            key += smallLetters[rnd.Next(0, smallLetters.Length - 1)].ToString();
            key += numbers[rnd.Next(0, numbers.Length - 1)].ToString();

            for (int i = 0; i < passwordLength - 3; i++)
            {
                key += allChars[rnd.Next(0, allChars.Length - 1)].ToString();
            }

            return key;
        }

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        } 

        private string Sha1(string input)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] data = sha1Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)

            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}