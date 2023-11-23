const crypto = require("crypto");


const array = new Uint8Array(1);
for(let i = 0 ; i < 5 ; i ++){
    console.log(crypto.getRandomValues(array));
}

const lastName = "Турчинович";

// Генерация ключей RSA
const { publicKey, privateKey } = crypto.generateKeyPairSync('rsa', {
    modulusLength: 2048,
});

// Шифрование фамилии с использованием открытого ключа
const encryptedLastName = crypto.publicEncrypt(publicKey, Buffer.from(lastName));
console.log("Encrypted Last Name: ", encryptedLastName.toString("base64"));

// Дешифрование фамилии с использованием закрытого ключа
const decryptedLastName = crypto.privateDecrypt(privateKey, encryptedLastName);
console.log("Decrypted Last Name: ", decryptedLastName.toString());

// Хеширование
const hash = crypto.createHash('sha256');
hash.update(lastName);
const hashedLastName = hash.digest('hex');
console.log("Hashed Last Name: ", hashedLastName);

// Генерация симметричного ключа для AES-KW
const symmetricKey = crypto.randomBytes(32); // 256 бит

// Упаковка симметричного ключа с использованием AES-KW
const wrappedKey = crypto.publicEncrypt(publicKey, symmetricKey);
console.log("Wrapped Key: ", wrappedKey.toString("base64"));

// Распаковка симметричного ключа с использованием AES-KW и закрытого ключа
const unwrappedKey = crypto.privateDecrypt(privateKey, wrappedKey);
console.log("Unwrapped Key: ", unwrappedKey.toString("hex"));

// Подпись сообщения с использованием закрытого ключа RSA
const sign = crypto.sign('sha256', Buffer.from(hashedLastName), {
    key: privateKey,
    padding: crypto.constants.RSA_PKCS1_PSS_PADDING,
    saltLength: 32, // Длина соли
});
console.log("Signature: ", sign.toString("base64"));

// Проверка подписи с использованием открытого ключа RSA
const isVerified = crypto.verify(
    'sha256',
    Buffer.from(hashedLastName),
    {
        key: publicKey,
        padding: crypto.constants.RSA_PKCS1_PSS_PADDING,
        saltLength: 32,
    },
    sign
);

if (isVerified) {
    console.log("Signature verified");
} else {
    console.log("Signature verification failed");
}
