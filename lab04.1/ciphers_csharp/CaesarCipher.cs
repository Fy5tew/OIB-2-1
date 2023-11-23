using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciphers_csharp {
    internal static class CaesarCipher {
        private const string CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string _caesarCipher(string oldMessage, int key, string alphabet) {
            string newMessage = "";
            foreach (char oldCh in oldMessage) {
                string currentAlphabet = char.IsUpper(oldCh) ? alphabet.ToUpper() : alphabet.ToLower();
                int oldChIndex = currentAlphabet.IndexOf(oldCh);
                char newCh;
                if (oldChIndex == -1) {
                    newCh = oldCh;
                }
                else {
                    int newChIndex = (oldChIndex + key) % currentAlphabet.Length;
                    if (newChIndex < 0) {
                        newChIndex = currentAlphabet.Length + newChIndex;
                    }
                    newCh = currentAlphabet[newChIndex];
                }
                newMessage += newCh;
            }
            return newMessage;
        }

        public static string CaesarCipherEncryption(string decryptedMessage, int key, string alphabet) {
            return _caesarCipher(decryptedMessage, key, alphabet);
        }

        public static string CaesarCipherDecryption(string encryptedMessage, int key, string alphabet) {
            return _caesarCipher(encryptedMessage, -key, alphabet);
        }

        public static string CaesarCipherCyrillicEncryption(string decryptedMessage, int key) {
            return CaesarCipherEncryption(decryptedMessage, key, CYRILLIC_ALPHABET);
        }

        public static string CaesarCipherCyrillicDecryption(string encryptedMessage, int key) {
            return CaesarCipherDecryption(encryptedMessage, key, CYRILLIC_ALPHABET);
        }

        public static string CaesarCipherLatinEncryption(string decryptedMessage, int key) {
            return CaesarCipherEncryption(decryptedMessage, key, LATIN_ALPHABET);
        }

        public static string CaesarCipherLatinDecryption(string encryptedMessage, int key) {
            return CaesarCipherDecryption(encryptedMessage, key, LATIN_ALPHABET);
        }
    }
}
