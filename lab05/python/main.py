import rsa


def create_keys():
    return rsa.newkeys(512)


def sign_message(message, private_key):
    return rsa.sign(message.encode('utf-8'), private_key, 'SHA-256')


def verify_signature(message, signature, public_key):
    try:
        rsa.verify(message.encode('utf-8'), signature, public_key)
        return True
    except rsa.VerificationError:
        return False


def main():
    real_message = "Fy5tew's secret message"
    fake_message = "Nikita's secret message"

    (public_key, private_key) = create_keys()
    signature = sign_message(real_message, private_key)

    real_message_verification = verify_signature(real_message, signature, public_key)
    fake_message_verification = verify_signature(fake_message, signature, public_key)

    print(f"Подпись: {signature}")
    print(f"Проверка настоящего сообщения: {real_message_verification}")
    print(f"Проверка поддельного сообщения: {fake_message_verification}")


if __name__ == '__main__':
    main()
