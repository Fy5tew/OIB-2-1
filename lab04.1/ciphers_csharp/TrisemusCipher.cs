using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciphers_csharp {
    internal static class TrisemusCipher {
        private const string CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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
                for (int j = 0;  j < table[i].Count; j++) {
                    if (table[i][j] == ch) {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        private static string _trisemusCipher(string oldMessage, string keyword, string alphabet, int step, int? tableSize = null) {
            List<List<char>> tableUpper = _makeTable(keyword, alphabet.ToUpper(), tableSize);
            List<List<char>> tableLower = _makeTable(keyword, alphabet.ToLower(), tableSize);
            step = (int)(step / Math.Abs(step));
            string newMessage = "";
            foreach (char oldCh in oldMessage) {
                List<List<char>> currentTable = char.IsUpper(oldCh) ? tableUpper : tableLower;
                (int, int) oldChIndex = _findTableIndex(oldCh, currentTable);
                char newCh;
                if (oldChIndex == (-1, -1)) {
                    newCh = oldCh;
                }
                else {
                    int newChIndexI = oldChIndex.Item1 + step;
                    int newChIndexJ = oldChIndex.Item2;
                    if (newChIndexI == -1) {
                        newChIndexI = currentTable.Count - 1;
                    }
                    if (newChIndexI >= currentTable.Count || newChIndexJ >= currentTable[newChIndexI].Count) {
                        newChIndexI = 0;
                    }
                    newCh = currentTable[newChIndexI][newChIndexJ];
                }
                newMessage += newCh;
            }
            return newMessage;
        }

        public static string TrisemusCipherEncryption(string decryptedMessage, string keyword, string alphabet, int? tableSize = null) {
            return _trisemusCipher(decryptedMessage, keyword, alphabet, 1, tableSize);
        }

        public static string TrisemusCipherDecryption(string encryptedMessage, string keyword, string alphabet, int? tableSize = null) {
            return _trisemusCipher(encryptedMessage, keyword, alphabet, -1, tableSize);
        }

        public static string TrisemusCipherCyrillicEncryption(string decryptedMessage, string keyword, int? tableSize = null) {
            return TrisemusCipherEncryption(decryptedMessage, keyword, CYRILLIC_ALPHABET, tableSize);
        }

        public static string TrisemusCipherCyrillicDecryption(string encryptedMessage, string keyword, int? tableSize = null) {
            return TrisemusCipherDecryption(encryptedMessage, keyword, CYRILLIC_ALPHABET, tableSize);
        }

        public static string TrisemusCipherLatinEncryption(string decryptedMessage, string keyword, int? tableSize = null) {
            return TrisemusCipherEncryption(decryptedMessage, keyword, LATIN_ALPHABET, tableSize);
        }

        public static string TrisemusCipherLatinDecryption(string encryptedMessage, string keyword, int? tableSize = null) {
            return TrisemusCipherDecryption(encryptedMessage, keyword, LATIN_ALPHABET, tableSize);
        }
    }
}
