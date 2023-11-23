CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"


def _get_key(message: str, keyword: str) -> str:
    key = ""
    while len(key) < len(message):
        key += keyword
    return key[:len(message)]


def _vigenere_cipher(*, old_message: str, keyword: str, key: int, alphabet: str) -> str:
    full_key = _get_key(old_message, keyword)
    new_message = ""
    for (msg_ch, key_ch) in zip(old_message, full_key):
        msg_alphabet = alphabet.upper() if msg_ch.isupper() else alphabet.lower()
        key_alphabet = alphabet.upper() if key_ch.isupper() else alphabet.lower()
        msg_ch_index = msg_alphabet.find(msg_ch)
        key_ch_index = key_alphabet.find(key_ch)
        if msg_ch_index == -1:
            new_ch = msg_ch
        else:
            new_ch_index = (msg_ch_index + key * key_ch_index) % len(alphabet)
            new_ch = msg_alphabet[new_ch_index]
        new_message += new_ch
    return new_message


def vigenere_cipher_encryption(*, decrypted_message: str, keyword: str, key: int, alphabet: str) -> str:
    return _vigenere_cipher(
        old_message=decrypted_message,
        keyword=keyword,
        key=key,
        alphabet=alphabet,
    )


def vigenere_cipher_decryption(*, encrypted_message: str, keyword: str, key: int, alphabet: str) -> str:
    return _vigenere_cipher(
        old_message=encrypted_message,
        keyword=keyword,
        key=-key,
        alphabet=alphabet,
    )


def vigenere_cipher_cyrillic_encryption(decrypted_message: str, keyword: str, key: int) -> str:
    return vigenere_cipher_encryption(
        decrypted_message=decrypted_message,
        keyword=keyword,
        key=key,
        alphabet=CYRILLIC_ALPHABET,
    )


def vigenere_cipher_cyrillic_decryption(encrypted_message: str, keyword: str, key: int) -> str:
    return vigenere_cipher_decryption(
        encrypted_message=encrypted_message,
        keyword=keyword,
        key=key,
        alphabet=CYRILLIC_ALPHABET,
    )


def vigenere_cipher_latin_encryption(decrypted_message: str, keyword: str, key: int) -> str:
    return vigenere_cipher_encryption(
        decrypted_message=decrypted_message,
        keyword=keyword,
        key=key,
        alphabet=LATIN_ALPHABET,
    )


def vigenere_cipher_latin_decryption(encrypted_message: str, keyword: str, key: int) -> str:
    return vigenere_cipher_decryption(
        encrypted_message=encrypted_message,
        keyword=keyword,
        key=key,
        alphabet=LATIN_ALPHABET,
    )
