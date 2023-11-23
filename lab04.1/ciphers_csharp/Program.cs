using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciphers_csharp {
    internal class Program {
        const int SECRET_KEY = 29;
        const string KEYWORD = "Защита";
        const string MESSAGE = "Турчинович Никита Александрович";

        static void WriteResult(string info, string message, string encryptedMessage, string decryptedMessage) {
            Console.WriteLine($"\t{info}\nMESSAGE: {message}\nENCRYPTED: {encryptedMessage}\nDECRYPTED: {decryptedMessage}\nEQUALS: {message == decryptedMessage}\n");
        }

        static void Main(string[] args) {
            string caesarEncrypted = CaesarCipher.CaesarCipherCyrillicEncryption(MESSAGE, SECRET_KEY);
            string caesarDecrypted = CaesarCipher.CaesarCipherCyrillicDecryption(caesarEncrypted, SECRET_KEY);

            string trisemusEncrypted = TrisemusCipher.TrisemusCipherCyrillicEncryption(MESSAGE, KEYWORD);
            string trisemusDecrypted = TrisemusCipher.TrisemusCipherCyrillicDecryption(trisemusEncrypted, KEYWORD);

            string playferEncrypted = PlayferCipher.PlayferCipherCyrillicEncryption(MESSAGE, KEYWORD);
            string playferDecrypted = PlayferCipher.PlayferCipherCyrillicDecryption(playferEncrypted, KEYWORD);

            string vigenereEncrypted = VigenereCipher.VigenereCipherCyrillicEncryption(MESSAGE, KEYWORD, SECRET_KEY);
            string vigenereDecrypted = VigenereCipher.VigenereCipherCyrillicDecryption(vigenereEncrypted, KEYWORD, SECRET_KEY);

            WriteResult($"[CAESAR CIPHER] {{ key={SECRET_KEY} }}", MESSAGE, caesarEncrypted, caesarDecrypted);
            WriteResult($"[TRISEMUS CIPHER] {{ keyword='{KEYWORD}' }}", MESSAGE, trisemusEncrypted, trisemusDecrypted);
            WriteResult($"[PLAYFER CIPHER] {{ keyword='{KEYWORD}' }}", MESSAGE, playferEncrypted, playferDecrypted);
            WriteResult($"[VIGENERE CIPHER] {{ keyword='{KEYWORD}' key={SECRET_KEY} }}", MESSAGE, vigenereEncrypted, vigenereDecrypted);
        }
    }
}
