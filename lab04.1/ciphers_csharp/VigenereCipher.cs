using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciphers_csharp {
    internal static class VigenereCipher {
        private const string CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string _getKey(string message, string keyword) {
            string key = "";
            while (key.Length < message.Length) {
                key += keyword;
            }
            return key.Substring(0, message.Length);
        }

        private static string _vigenereCipher(string oldMessage, string keyword, int key, string alphabet) {
            string fullKey = _getKey(oldMessage, keyword);
            string newMessage = "";
            for (int i = 0; i < oldMessage.Length; i++) {
                char msgCh = oldMessage[i];
                char keyCh = fullKey[i];
                string msgAlphabet = char.IsUpper(msgCh) ? alphabet.ToUpper() : alphabet.ToLower();
                string keyAlphabet = char.IsUpper(keyCh) ? alphabet.ToUpper(): alphabet.ToLower();
                int msgChIndex = msgAlphabet.IndexOf(msgCh);
                int keyChIndex = keyAlphabet.IndexOf(keyCh);
                char newCh;
                if (msgChIndex == -1) {
                    newCh = msgCh;
                }
                else {
                    int newChIndex = (msgChIndex + key * keyChIndex) % alphabet.Length;
                    if (newChIndex < 0) {
                        newChIndex = alphabet.Length + newChIndex;
                    }
                    newCh = msgAlphabet[newChIndex];
                }
                newMessage += newCh;
            }
            return newMessage;
        }

        public static string VigenereCipherEncryption(string decryptedMessage, string keyword, int key, string alphabet) {
            return _vigenereCipher(
                decryptedMessage,
                keyword,
                key,
                alphabet
            );
        }

        public static string VigenereCipherDecryption(string encryptedMessage, string keyword, int key, string alphabet) {
            return _vigenereCipher(
                encryptedMessage,
                keyword,
                -key,
                alphabet
            );
        }

        public static string VigenereCipherCyrillicEncryption(string decryptedMessage, string keyword, int key) {
            return VigenereCipherEncryption(
                decryptedMessage,
                keyword,
                key,
                CYRILLIC_ALPHABET
            );
        }

        public static string VigenereCipherCyrillicDecryption(string encryptedMessage, string keyword, int key) {
            return VigenereCipherDecryption(
                encryptedMessage,
                keyword,
                key,
                CYRILLIC_ALPHABET
            );
        }

        public static string VigenereCipherLatinEncryption(string decryptedMessage, string keyword, int key) {
            return VigenereCipherEncryption(
                decryptedMessage,
                keyword,
                key,
                LATIN_ALPHABET
            );
        }

        public static string VigenereCipherLatinDecryption(string encryptedMessage, string keyword, int key) {
            return VigenereCipherDecryption(
                encryptedMessage,
                keyword,
                key,
                LATIN_ALPHABET
            );
        }
    }

}
