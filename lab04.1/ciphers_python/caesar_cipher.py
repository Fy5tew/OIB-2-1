CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"


def _caesar_cipher(*, old_message: str, key: int, alphabet: str) -> str:
    new_message = ""
    for old_ch in old_message:
        current_alphabet = alphabet.upper() if old_ch.isupper() else alphabet.lower()
        old_ch_index = current_alphabet.find(old_ch)
        if (old_ch_index == -1):
            new_ch = old_ch
        else:
            new_ch_index = (old_ch_index + key) % len(current_alphabet)
            new_ch = current_alphabet[new_ch_index]
        new_message += new_ch
    return new_message


def caesar_cipher_encryption(*, decrypted_message: str, key: int, alphabet: str) -> str:
    return _caesar_cipher(
        old_message=decrypted_message, 
        key=key, 
        alphabet=alphabet,
    )


def caesar_cipher_decryption(*, encrypted_message: str, key: int, alphabet: str) -> str:
    return _caesar_cipher(
        old_message=encrypted_message, 
        key=-key, 
        alphabet=alphabet,
    )


def caesar_cipher_cyrillic_encryption(decrypted_message: str, key: int) -> str:
    return caesar_cipher_encryption(
        decrypted_message=decrypted_message, 
        key=key, 
        alphabet=CYRILLIC_ALPHABET,
    )


def caesar_cipher_cyrillic_decryption(encrypted_message: str, key: int) -> str:
    return caesar_cipher_decryption(
        encrypted_message=encrypted_message, 
        key=key, 
        alphabet=CYRILLIC_ALPHABET,
    )


def caesar_cipher_latin_encryption(decrypted_message: str, key: int) -> str:
    return caesar_cipher_encryption(
        decrypted_message=decrypted_message,
        key=key, 
        alphabet=LATIN_ALPHABET,
    )


def caesar_cipher_latin_decryption(encrypted_message: str, key: int) -> str:
    return caesar_cipher_decryption(
        encrypted_message=encrypted_message, 
        key=key, 
        alphabet=LATIN_ALPHABET,
    )
