CYRILLIC_ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
LATIN_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"


def _get_table_chars(*, keyword: str, alphabet: str) -> list[str]:
    table_chars = []
    for ch in (keyword + alphabet):
        if ch not in table_chars:
            table_chars.append(ch)
    return table_chars


def _make_table(*, keyword: str, alphabet: str, table_size: int | None = None) -> list[list[str]]:
    table_chars = _get_table_chars(keyword=keyword, alphabet=alphabet)
    if table_size is None:
        table_size = len(set(keyword))
    table = [[]]
    for ch in table_chars:
        if len(table[-1]) >= table_size:
            table.append([])
        table[-1].append(ch)
    return table


def _find_table_index(ch: str, table: list[list[str]]) -> int | tuple[int, int]:
    for i in range(len(table)):
        for j in range(len(table[i])):
            if table[i][j] == ch:
                return (i, j)
    return -1


def _trisemus_cipher(*, old_message: str, keyword: str, alphabet: str, step: int, table_size: int | None = None) -> str:
    table_upper = _make_table(
        keyword=keyword, 
        alphabet=alphabet.upper(), 
        table_size=table_size
    )
    table_lower = _make_table(
        keyword=keyword, 
        alphabet=alphabet.lower(), 
        table_size=table_size
    )
    step = int(step / abs(step))
    new_message = ""
    for old_ch in old_message:
        current_table = table_upper if old_ch.isupper() else table_lower
        old_ch_index = _find_table_index(old_ch, current_table)
        if old_ch_index == -1:
            new_ch = old_ch
        else:
            new_ch_index_i = old_ch_index[0] + step
            new_ch_index_j = old_ch_index[1]
            if new_ch_index_i == -1:
                new_ch_index_i = len(current_table) - 1
            if new_ch_index_i >= len(current_table) or new_ch_index_j >= len(current_table[new_ch_index_i]):
                new_ch_index_i = 0
            new_ch = current_table[new_ch_index_i][new_ch_index_j]
        new_message += new_ch
    return new_message


def trisemus_cipher_encryption(*, decrypted_message: str, keyword: str, alphabet: str, table_size: int | None = None) -> str:
    return _trisemus_cipher(
        old_message=decrypted_message, 
        keyword=keyword, 
        alphabet=alphabet,
        step=1,
        table_size=table_size,
    )


def trisemus_cipher_decryption(*, encrypted_message: str, keyword: str, alphabet: str, table_size: int | None = None) -> str:
    return _trisemus_cipher(
        old_message=encrypted_message, 
        keyword=keyword, 
        alphabet=alphabet,
        step=-1,
        table_size=table_size,
    )


def trisemus_cipher_cyrillic_encryption(decrypted_message: str, keyword: str, table_size: int | None = None) -> str:
    return trisemus_cipher_encryption(
        decrypted_message=decrypted_message,
        keyword=keyword,
        alphabet=CYRILLIC_ALPHABET,
        table_size=table_size,
    )


def trisemus_cipher_cyrillic_decryption(encrypted_message: str, keyword: str, table_size: int | None = None) -> str:
    return trisemus_cipher_decryption(
        encrypted_message=encrypted_message,
        keyword=keyword,
        alphabet=CYRILLIC_ALPHABET,
        table_size=table_size,
    )


def trisemus_cipher_latin_encryption(decrypted_message: str, keyword: str, table_size: int | None = None) -> str:
    return trisemus_cipher_encryption(
        decrypted_message=decrypted_message,
        keyword=keyword,
        alphabet=LATIN_ALPHABET,
        table_size=table_size,
    )


def trisemus_cipher_latin_decryption(encrypted_message: str, keyword: str, table_size: int | None = None) -> str:
    return trisemus_cipher_decryption(
        encrypted_message=encrypted_message,
        keyword=keyword,
        alphabet=LATIN_ALPHABET,
        table_size=table_size,
    )
