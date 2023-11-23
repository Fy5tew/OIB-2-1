using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciphers_csharp {
    internal static class PlayferCipher {
        private const string CYRILLIC_ALPHABET = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string LATIN_ALPHABET = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

        private static List<char> _getTableChars(string keyword, string alphabet) {
            List<char> tableChars = new List<char>();
            foreach (char ch in (keyword + alphabet)) {
                if (!tableChars.Contains(ch)) {
                    tableChars.Add(ch);
                }
            }
            return tableChars;
        }

        private static List<List<char>> _makeTable(string keyword, string alphabet, int? tableSize = null) {
            List<char> tableChars = _getTableChars(keyword, alphabet);
            if (tableSize == null) {
                tableSize = keyword.Distinct().ToList().Count;
            }
            List<List<char>> table = new List<List<char>>() { new List<char>() };
            foreach (char ch in tableChars) {
                if (table.Last().Count >= tableSize) {
                    table.Add(new List<char>());
                }
                table.Last().Add(ch);
            }
            return table;
        }

        private static (int, int) _findTableIndex(char ch, List<List<char>> table) {
            for (int i = 0; i < table.Count; i++) {
                for (int j = 0; j < table[i].Count; j++) {
                    if (table[i][j] == ch) {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        private static string _getDigrams(string message, char delimeter, string alphabet) {
            int digramLetters = 0;
            char lastLetter = '\0';
            string digrams = "";
            foreach (char ch in message.ToUpper()) {
                if (!alphabet.ToUpper().Contains(ch)) {
                    continue;
                }
                if (digramLetters >= 2) {
                    digrams += ' ';
                    digramLetters = 0;
                }
                if (digramLetters == 1 && ch == lastLetter) {
                    digrams += delimeter + ' ';
                    digramLetters = 0;
                }
                digrams += ch;
                lastLetter = ch;
                digramLetters++;
            }
            if (digramLetters == 1) {
                digrams += delimeter;
            }
            return digrams;
        }

        private static (int, int) _normalizeIndex((int, int) index, List<List<char>> table) {
            (int, int) newIndex = (index.Item1, index.Item2);
            if (newIndex.Item1 >= table.Count) {
                newIndex.Item1 = 0;
            }
            else if (newIndex.Item1 < 0) {
                newIndex.Item1 = table.Count - 1;
            }
            if (newIndex.Item2 >= table[newIndex.Item1].Count) {
                newIndex.Item2 = 0;
            }
            else if (newIndex.Item2 < 0) {
                newIndex.Item2 = table[newIndex.Item1].Count - 1;
            }
            return newIndex;
        }

        private static string _playferCipher(string oldMessage, string keyword, string alphabet, char helpChar, int step, int tableSize) {
            List<List<char>> table = _makeTable(
                keyword.ToUpper(),
                alphabet.ToUpper(),
                tableSize
            );
            step = (int)(step / Math.Abs(step));
            string oldDigrams = _getDigrams(oldMessage, helpChar, alphabet);
            string newDigrams = "";
            foreach (string oldDigram in oldDigrams.Split(' ')) {
                (int, int) oldCh1Index = _findTableIndex(oldDigram[0], table);
                (int, int) oldCh2Index = _findTableIndex(oldDigram[1], table);
                (int, int) newCh1Index;
                (int, int) newCh2Index;
                if (oldCh1Index.Item1 == oldCh2Index.Item1) {
                    newCh1Index = (oldCh1Index.Item1, oldCh1Index.Item2 + step);
                    newCh2Index = (oldCh2Index.Item1, oldCh2Index.Item2 + step);
                }
                else if (oldCh1Index.Item2 == oldCh2Index.Item2) {
                    newCh1Index = (oldCh1Index.Item1 + step, oldCh1Index.Item2);
                    newCh2Index = (oldCh2Index.Item1 + step, oldCh2Index.Item2);
                }
                else {
                    newCh1Index = (oldCh1Index.Item1, oldCh2Index.Item2);
                    newCh2Index = (oldCh2Index.Item1, oldCh1Index.Item2);
                }
                newCh1Index = _normalizeIndex(newCh1Index, table);
                newCh2Index = _normalizeIndex(newCh2Index, table);
                string newDigram = string.Concat(table[newCh1Index.Item1][newCh1Index.Item2], table[newCh2Index.Item1][newCh2Index.Item2]);
                newDigrams += ' ' + newDigram;
            }
            return newDigrams.Replace(" ", "");
        }

        public static string PlayferCipherEncryption(string decryptedMessage, string keyword, string alphabet, char helpChar, int tableSize) {
            return _playferCipher(
                decryptedMessage,
                keyword,
                alphabet,
                helpChar,
                1,
                tableSize
            );
        }

        public static string PlayferCipherDecryption(string encryptedMessage, string keyword, string alphabet, char helpChar, int tableSize) {
            return _playferCipher(
                encryptedMessage,
                keyword,
                alphabet,
                helpChar,
                -1,
                tableSize
            );
        }

        public static string PlayferCipherCyrillicEncryption(string decryptedMessage, string keyword) {
            return PlayferCipherEncryption(
                decryptedMessage.Replace('ё', 'е').Replace('Ё', 'Е'),
                keyword,
                CYRILLIC_ALPHABET,
                'Я',
                4
            );
        }

        public static string PlayferCipherCyrillicDecryption(string encryptedMessage, string keyword) {
            return PlayferCipherDecryption(
                encryptedMessage,
                keyword,
                CYRILLIC_ALPHABET,
                'Я',
                4
            );
        }

        public static string PlayferCipherLatinEncryption(string decryptedMessage, string keyword) {
            return PlayferCipherEncryption(
                decryptedMessage.Replace('j', 'i').Replace('J', 'I'),
                keyword,
                LATIN_ALPHABET,
                'X',
                5
            );
        }

        public static string PlayferCipherLatinDecryption(string encryptedMessage, string keyword) {
            return PlayferCipherDecryption(
                encryptedMessage,
                keyword,
                LATIN_ALPHABET,
                'X',
                5
            );
        }
    }
}
