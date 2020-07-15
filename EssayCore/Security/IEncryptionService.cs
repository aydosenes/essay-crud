using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essay.Core
{
    public interface IEncryptionService
    {
        string PrivateKey(int id);
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");
        string EncryptText(string plainText, string encryptionPrivateKey = "");
        string DecryptText(string cipherText, string encryptionPrivateKey = "");
        string CreatePasswordSHA512(string text);
        string CreatePassword(int passwordLength);
    }
}
