CYRILLIC_ALPHABET = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"
LATIN_ALPHABET = "ABCDEFGHIKLMNOPQRSTUVWXYZ"


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


def _get_digrams(message: str, help_char: str, alphabet: str) -> str:
    digram_letters = 0
    last_letter = ''
    digrams = ""
    for ch in message.upper():
        if ch not in alphabet.upper():
            continue
        if digram_letters >= 2:
            digrams += ' '
            digram_letters = 0
        digrams += ch
        last_letter = ch
        digram_letters += 1
    if digram_letters == 1:
        digrams += help_char
    return digrams


def _normalize_index(index: tuple[int, int], table: list[list[str]]) -> tuple[int, int]:
    new_index = [index[0], index[1]]
    if new_index[0] >= len(table):
        new_index[0] = 0
    elif new_index[0] < 0:
        new_index[0] = len(table) - 1
    if new_index[1] >= len(table[new_index[0]]):
        new_index[1] = 0
    elif new_index[1] < 0:
        new_index[1] = len(table[new_index[0]]) - 1
    return (new_index[0], new_index[1])


def _wheatstone_cipher(*, old_message: str, keywords: tuple[str, str], alphabet: str, help_char: str, step: int, table_size: int) -> str:
    tables = tuple(_make_table(keyword=keyword.upper(), alphabet=alphabet.upper(), table_size=table_size) for keyword in keywords)
    old_digrams = _get_digrams(old_message, help_char, alphabet)
    step = int(step / abs(step))
    new_digrams = ""
    for old_digram in old_digrams.split(' '):
        old_ch_1_index = _find_table_index(old_digram[0], tables[1 if step == 1 else 0])
        old_ch_2_index = _find_table_index(old_digram[1], tables[0 if step == 1 else 1])
        if (old_ch_1_index[0] == old_ch_2_index[0]):
            new_ch_1_index = (old_ch_1_index[0], old_ch_2_index[1]) if step == 1 else (old_ch_2_index[0], old_ch_1_index[1])
            new_ch_2_index = (old_ch_2_index[0], old_ch_1_index[1]) if step == 1 else (old_ch_1_index[0], old_ch_2_index[1])
        else:
            new_ch_1_index = (old_ch_1_index[0], old_ch_2_index[1])
            new_ch_2_index = (old_ch_2_index[0], old_ch_1_index[1])
        new_ch_1_index = _normalize_index(new_ch_1_index, tables[1])
        new_ch_2_index = _normalize_index(new_ch_2_index, tables[0])
        new_digram = tables[0 if step == 1 else 1][new_ch_1_index[0]][new_ch_1_index[1]] + tables[1 if step == 1 else 0][new_ch_2_index[0]][new_ch_2_index[1]]
        new_digrams += ' ' + new_digram
    return new_digrams.strip()


def wheatstone_cipher_encryption(*, decrypted_message: str, keywords: tuple[str, str], alphabet: str, help_char: str, table_size: int):
    return _wheatstone_cipher(
        old_message=decrypted_message,
        keywords=keywords,
        alphabet=alphabet,
        help_char=help_char,
        step=1,
        table_size=table_size,
    )


def wheatstone_cipher_decryption(*, encrypted_message: str, keywords: tuple[str, str], alphabet: str, help_char: str, table_size: int):
    return _wheatstone_cipher(
        old_message=encrypted_message,
        keywords=keywords,
        alphabet=alphabet,
        help_char=help_char,
        step=-1,
        table_size=table_size,
    )


def wheatstone_cipher_cyrillic_encryption(decrypted_message: str, keywords: tuple[str, str]):
    return wheatstone_cipher_encryption(
        decrypted_message=decrypted_message,
        keywords=keywords,
        alphabet=CYRILLIC_ALPHABET,
        help_char='Ъ',
        table_size=8,
    )


def wheatstone_cipher_cyrillic_decryption(encrypted_message: str, keywords: tuple[str, str]):
    return wheatstone_cipher_decryption(
        encrypted_message=encrypted_message,
        keywords=keywords,
        alphabet=CYRILLIC_ALPHABET,
        help_char='Ъ',
        table_size=8,
    )


def wheatstone_cipher_latin_encryption(decrypted_message: str, keywords: tuple[str, str]):
    return wheatstone_cipher_encryption(
        decrypted_message=decrypted_message,
        keywords=keywords,
        alphabet=LATIN_ALPHABET,
        help_char='X',
        table_size=5,
    )


def wheatstone_cipher_latin_decryption(encrypted_message: str, keywords: tuple[str, str]):
    return wheatstone_cipher_decryption(
        encrypted_message=encrypted_message,
        keywords=keywords,
        alphabet=LATIN_ALPHABET,
        help_char='X',
        table_size=5,
    )
