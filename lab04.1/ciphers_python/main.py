from caesar_cipher import (
    caesar_cipher_cyrillic_encryption,
    caesar_cipher_cyrillic_decryption,
)
from trisemus_cipher import (
    trisemus_cipher_cyrillic_encryption,
    trisemus_cipher_cyrillic_decryption,
)
from playfer_cipher import (
    playfer_cipher_cyrillic_encryption,
    playfer_cipher_cyrillic_decryption,
)
from vigenere_cipher import (
    vigenere_cipher_cyrillic_encryption,
    vigenere_cipher_cyrillic_decryption,
)
from wheatstone_cipher import wheatstone_cipher_cyrillic_decryption


SECRET_KEY = 29
KEYWORD = "Защита"
MESSAGE = "Турчинович Никита Александрович"


def print_result(info: str, message: str, encrypted_message: str, decrypted_message: str) -> None:
    print(f"\t{info}\nMESSAGE: {message}\nENCRYPTED: {encrypted_message}\nDECRYPTED: {decrypted_message}\nEQUALS: {message == decrypted_message}\n")


def main():
    caesar_encrypted = caesar_cipher_cyrillic_encryption(MESSAGE, SECRET_KEY)
    caesar_decrypted = caesar_cipher_cyrillic_decryption(caesar_encrypted, SECRET_KEY)

    trisemus_encrypted = trisemus_cipher_cyrillic_encryption(MESSAGE, KEYWORD)
    trisemus_decrypted = trisemus_cipher_cyrillic_decryption(trisemus_encrypted, KEYWORD)

    playfer_encrypted = playfer_cipher_cyrillic_encryption(MESSAGE, KEYWORD)
    playfer_decrypted = playfer_cipher_cyrillic_decryption(playfer_encrypted, KEYWORD)

    vigenere_encrypted = vigenere_cipher_cyrillic_encryption(MESSAGE, KEYWORD, SECRET_KEY)
    vigenere_decrypted = vigenere_cipher_cyrillic_decryption(vigenere_encrypted, KEYWORD, SECRET_KEY)

    print_result(f"[CAESAR CIPHER] {{ key={SECRET_KEY} }}", MESSAGE, caesar_encrypted, caesar_decrypted)
    print_result(f"[TRISEMUS CIPHER] {{ keyword='{KEYWORD}' }}'", MESSAGE, trisemus_encrypted, trisemus_decrypted)
    print_result(f"[PLAYFER CIPHER] {{ keyword='{KEYWORD}' }}", MESSAGE, playfer_encrypted, playfer_decrypted)
    print_result(f"[VIGENERE CIPHER] {{ keyword='{KEYWORD}' key={SECRET_KEY} }}", MESSAGE, vigenere_encrypted, vigenere_decrypted)

    print(f"\t[VARIANT TASK {SECRET_KEY}]")
    print("пх кю гй яг зо ад зн йр юм тш иь")
    print(wheatstone_cipher_cyrillic_decryption("пх кю гй яг зо ад зн йр юм тш иь", ("ХАЛЯВА", "РАБОТА")))


if __name__ == '__main__':
    main()
