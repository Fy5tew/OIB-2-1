using System.Net.Security;
using System.Security.Cryptography;
using System.Text;

namespace lab08_1 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = @"D:\Study\2c1s\ОИБ\lab08\lab08\lab08.1\Files";
            string surname = "Турчинович";

            Console.WriteLine($"[Инициализация] Исходный текст: {surname}");
            Console.WriteLine();

            //      ШИФРОВАНИЕ
            // Создание AES провайдера
            using (var aes192 = Aes.Create()) {
                // Установка параметров
                aes192.KeySize = 192;
                aes192.GenerateKey();
                aes192.GenerateIV();

                // Получение ключей
                var aes192Key = Convert.ToBase64String(aes192.Key);
                var aes192IV = Convert.ToBase64String(aes192.IV);

                // Шифрование
                var bytesToEncrypt = Encoding.UTF8.GetBytes(surname);
                byte[] encryptedBytes;
                using (var aes192Encryptor = aes192.CreateEncryptor(aes192.Key, aes192.IV)) {
                    encryptedBytes = aes192Encryptor.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
                }
                var encryptedData = Convert.ToBase64String(encryptedBytes);

                // Запись данных в файл
                File.WriteAllText(Path.Combine(filePath, "aes192Key.txt"), aes192Key);
                File.WriteAllText(Path.Combine(filePath, "aes192IV.txt"), aes192IV);
                File.WriteAllText(Path.Combine(filePath, "encryptedData.txt"), encryptedData);

                // Вывод данных в консоль
                Console.WriteLine($"[Шифрование] Ключ: {aes192Key}");
                Console.WriteLine($"[Шифрование] Вектор: {aes192IV}");
                Console.WriteLine($"[Шифрование] Зашифрованный текст: {encryptedData}");
                Console.WriteLine();
            }

            //      ДЕШИФРОВАНИЕ
            // Создание AES провайдера
            using (var aes192 = Aes.Create()) {
                // Чтение данных
                var aes192Key = File.ReadAllText(Path.Combine(filePath, "aes192Key.txt"));
                var aes192IV = File.ReadAllText(Path.Combine(filePath, "aes192IV.txt"));
                var encryptedData = File.ReadAllText(Path.Combine(filePath, "encryptedData.txt"));

                // Установка параметров
                aes192.KeySize = 192;
                aes192.Key = Convert.FromBase64String(aes192Key);
                aes192.IV = Convert.FromBase64String(aes192IV);

                // Дешифрование
                var encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedBytes;
                using (var aes192Decryptor = aes192.CreateDecryptor(aes192.Key, aes192.IV)) {
                    decryptedBytes = aes192Decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                }
                var decryptedData = Encoding.UTF8.GetString(decryptedBytes);

                // Вывод данных в консоль
                Console.WriteLine($"[Дешифрование] Ключ: {aes192Key}");
                Console.WriteLine($"[Дешифрование] Вектор: {aes192IV}");
                Console.WriteLine($"[Дешифрование] Зашифрованный текст: {encryptedData}");
                Console.WriteLine($"[Дешифрование] Расшифрованный текст: {decryptedData}");
                Console.WriteLine();
            }

            //      ХЕШИРОВАНИЕ
            // Создание SHA провайдера
            using (var sha384 = SHA384.Create()) {
                // Хеширование
                var bytes = Encoding.UTF8.GetBytes(surname);
                var hashBytes = sha384.ComputeHash(bytes);
                var hash = Convert.ToBase64String(hashBytes);

                // Запись данных в файл
                File.WriteAllText(Path.Combine(filePath, "hash.txt"), hash);

                // Вывод в консоль
                Console.WriteLine($"[Хеширование] Хеш: {hash}");
                Console.WriteLine();
            }

            using (var aes192 = Aes.Create()) {
                var aes192Key = File.ReadAllText(Path.Combine(filePath, "aes192Key.txt"));
                var aes192IV = File.ReadAllText(Path.Combine(filePath, "aes192IV.txt"));
                var encryptedData = File.ReadAllText(Path.Combine(filePath, "encryptedData.txt"));

                // Установка параметров
                aes192.KeySize = 192;
                aes192.Key = Convert.FromBase64String(aes192Key);
                aes192.IV = Convert.FromBase64String(aes192IV);
            }
        }
    }
}