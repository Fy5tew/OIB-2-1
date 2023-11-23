import rsa
import elgamal
import diffiehellman


def rsa_alg(message):
    (public_key, private_key) = rsa.newkeys(512)
    encrypted = rsa.encrypt(message.encode('utf-8'), public_key)
    decrypted = rsa.decrypt(encrypted, private_key).decode('utf-8')

    print("\t\t[RSA]")
    print(" \t[Открытый ключ]")
    print(f"Модуль (n): {hex(public_key.n)}")
    print(f"Открытая экспонента: {hex(public_key.e)}")
    print(" \t[Закрытый ключ]")
    print(f"Модуль (n): {hex(private_key.n)}")
    print(f"Открытая экспонента: {hex(private_key.e)}")
    print("\t[Шифрование]")
    print(f"Исходное сообщение: \"{message}\"")
    print(f"Зашифрованное сообщение: \"{encrypted}\"")
    print(f"Расшифрованное сообщение: \"{decrypted}\"")
    print()


def elgamal_alg(message):
    keys = elgamal.generate_keys(512)
    public_key = keys['publicKey']
    private_key = keys['privateKey']
    encrypted = elgamal.encrypt(public_key, message)
    decrypted = elgamal.decrypt(private_key, encrypted)

    print("\t\t[Эль-Гамаля]")
    print("\t[Открытый ключ]")
    print(f"p: {hex(public_key.p)}")
    print(f"g: {hex(public_key.g)}")
    print(f"y: {hex(public_key.h)}")
    print("\t[Закрытый ключ]")
    print(f"p: {hex(private_key.p)}")
    print(f"g: {hex(private_key.g)}")
    print(f"x: {hex(private_key.x)}")
    print("\t[Шифрование]")
    print(f"Исходное сообщение: \"{message}\"")
    print(f"Зашифрованное сообщение: \"{encrypted}\"")
    print(f"Расшифрованное сообщение: \"{decrypted}\"")
    print()


def diffiehellman_alg():
    user1 = diffiehellman.DiffieHellman(key_bits=512)
    user2 = diffiehellman.DiffieHellman(key_bits=512)

    user1_public_key = user1.get_public_key()
    user1_private_key = user1.get_private_key()

    user2_public_key = user2.get_public_key()
    user2_private_key = user2.get_private_key()

    user1_shared_key = user1.generate_shared_key(user2_public_key)
    user2_shared_key = user2.generate_shared_key(user1_public_key)

    print("\t\t[Диффи-Хеллмана]")
    print("\t[Пользователь1]")
    print(f"Открытый ключ: {user1_public_key}")
    print(f"Закрытый ключ: {user1_private_key}")
    print("\t[Пользователь2]")
    print(f"Открытый ключ: {user2_public_key}")
    print(f"Закрытый ключ: {user2_private_key}")
    print("\t[Общий секретный ключ]")
    print(f"Пользователь1: {user1_shared_key}")
    print(f"Пользователь2: {user2_shared_key}")
    print(f"Равны: {user1_shared_key == user2_shared_key}")


def main():
    message = "Fy5tew's secret message"

    rsa_alg(message)
    elgamal_alg(message)
    diffiehellman_alg()


if __name__ == '__main__':
    main()
