using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class Program {
    public static void Main() {
        string labPath = @"D:\Study\2c1s\ОИБ\lab08\lab08\lab08.2\Files";
        // Ваша фамилия
        string surname = "Турчинович";

        // Создание RSA с 2048 битами
        using (var rsa = new RSACryptoServiceProvider(2048)) {
            // Получение ключей
            var publicKey = rsa.ExportParameters(false);
            var privateKey = rsa.ExportParameters(true);

            // Сохранение ключей в файлы
            File.WriteAllText(Path.Combine(labPath, "publicKey.txt"), Convert.ToBase64String(publicKey.Modulus));
            File.WriteAllText(Path.Combine(labPath, "privateKey.txt"), Convert.ToBase64String(privateKey.Modulus));

            // Шифрование
            var bytesToEncrypt = Encoding.UTF8.GetBytes(surname);
            var encryptedBytes = rsa.Encrypt(bytesToEncrypt, false);
            var encryptedData = Convert.ToBase64String(encryptedBytes);

            // Сохранение зашифрованных данных в файл
            File.WriteAllText(Path.Combine(labPath, "encryptedData.txt"), encryptedData);

            // Дешифрование
            var bytesToDecrypt = Convert.FromBase64String(encryptedData);
            var decryptedBytes = rsa.Decrypt(bytesToDecrypt, false);
            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);

            // Проверка, что дешифрованные данные совпадают с исходными
            Console.WriteLine("Decryption successful: " + (surname == decryptedData));
            Console.WriteLine("Encrypted Data: " + encryptedData);

            // Создание SHA провайдера
            using (var sha384 = SHA384.Create()) {
                var bytes = Encoding.UTF8.GetBytes(surname);
                var hashedBytes = sha384.ComputeHash(bytes);
                var hashStr = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Сохранение хеша в файл
                File.WriteAllText(Path.Combine(labPath, "hash.txt"), hashStr);

                Console.WriteLine("Hash: " + hashStr);
            }


            // Создание цифровой подписи

            // Импорт закрытого ключа
            rsa.ImportParameters(privateKey);

            // Чтение хеша из файла
            var hash = File.ReadAllText(Path.Combine(labPath, "hash.txt"));

            // Преобразование хеша в байты
            var hashBytes = Enumerable.Range(0, hash.Length / 2)
                .Select(x => Convert.ToByte(hash.Substring(x * 2, 2), 16))
                .ToArray();

            // Создание подписи на основе хеша
            var signature = rsa.SignHash(hashBytes, CryptoConfig.MapNameToOID("SHA384"));

            // Сохранение подписи в файл
            File.WriteAllText(Path.Combine(labPath, "signature.txt"), Convert.ToBase64String(signature));

            // Вывод подписи в консоль
            Console.WriteLine("Signature: " + Convert.ToBase64String(signature));


            // Проверка цифровой подписи

            // Импорт открытого ключа
            rsa.ImportParameters(publicKey);



            // Проверка подписи на основе хеша
            var isValid = rsa.VerifyHash(hashBytes, CryptoConfig.MapNameToOID("SHA384"), signature);

            // Вывод результата проверки в консоль
            Console.WriteLine("Signature is valid: " + isValid);


            // Демонстрация изменения хеша или сообщения

            // Импорт открытого ключа
            rsa.ImportParameters(publicKey);

            // Чтение хеша из файла



            // Изменение одного байта в хеше
            hashBytes[0] ^= 1;


            // Вывод результата проверки в консоль
            Console.WriteLine("Signature is valid after hash change: " + isValid);

            // Восстановление исходного хеша
            hashBytes[0] ^= 1;

            // Изменение одного байта в подписи
            signature[0] ^= 1;

            // Проверка подписи на основе измененной подписи
            isValid = rsa.VerifyHash(hashBytes, CryptoConfig.MapNameToOID("SHA384"), signature);

            // Вывод результата проверки в консоль
            Console.WriteLine("Signature is valid after signature change: " + isValid);


        }

        // Хеширование SHA512
        using (var sha512 = SHA512.Create()) {
            var bytes = Encoding.UTF8.GetBytes(surname);
            var hashBytes = sha512.ComputeHash(bytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // Сохранение хеша в файл
            File.WriteAllText(Path.Combine(labPath, "hash.txt"), hash);

            Console.WriteLine("Hash: " + hash);
        }
    }
}
