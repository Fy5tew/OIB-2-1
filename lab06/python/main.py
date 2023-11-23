from functools import reduce


# Находит разложение числа на простые множители
def get_prime_factors(n: int) -> list[int]:
    prime_factors = []
    i = 2
    while i * i <= n:
        while n % i == 0:
            prime_factors.append(i)
            n /= i
        i += 1
    if n > 1:
        prime_factors.append(int(n))
    return prime_factors


# Форматирует массив множителей в строку
def format_factors(factors: list[int], *, expanded: bool = False) -> str:
    factors_repeats = [
        (factor, 1 if expanded else factors.count(factor))
        for factor in (factors if expanded else set(factors))
    ]
    formatted = ' * '.join(map(
        lambda fr: f'{fr[0]}' if fr[1] == 1 else f"{fr[0]}^{fr[1]}",
        factors_repeats
    ))
    return formatted


# Находит НОД методом Евклида
def find_gcd_euclidean(a: int, b: int) -> int:
    while a != 0 and b != 0:
        if a > b:
            a = a % b
        else:
            b = b % a
    return a + b


# Находит НОД методом разложения на простые множители
def find_gcd_primes(a: int, b: int) -> int:
    a_primes = get_prime_factors(a)
    b_primes = get_prime_factors(b)
    common_primes = set(a_primes) & set(b_primes)
    return reduce(lambda acc, el: acc * el, common_primes, 1)


def extended_euclidean_algo(a, b):
    x, xx, y, yy = 1, 0, 0, 1
    while b:
        q = a // b
        a, b = b, a % b
        x, xx = xx, x - xx*q
        y, yy = yy, y - yy*q
    return (a, x, y)


def main():
    a = int(input("Введите число a: "))
    b = int(input("Введите число b: "))
    x = int(input("Введите число x: "))
    y = int(input("Введите число y: "))
    z = int(input("Введите число z: "))

    # Задание #1
    a_factors = get_prime_factors(a)
    b_factors = get_prime_factors(b)
    a_factors_expanded = format_factors(a_factors, expanded=True)
    b_factors_expanded = format_factors(b_factors, expanded=True)
    a_factors_shortened = format_factors(a_factors, expanded=False)
    b_factors_shortened = format_factors(b_factors, expanded=False)
    print("\n\t\tЗадание #1")
    print(f"a = {a} = {a_factors_expanded} = {a_factors_shortened}")
    print(f"b = {b} = {b_factors_expanded} = {b_factors_shortened}")

    # Задание #2
    gcd_euclidean = find_gcd_euclidean(a, b)
    gcd_primes = find_gcd_primes(a, b)
    print("\n\t\tЗадание #2")
    print(f"а) НОД({a}, {b}) = {gcd_euclidean}")
    print(f"б) НОД({a}, {b}) = {gcd_primes}")

    # Задание #3
    gcd, u, v = extended_euclidean_algo(a, b)
    print("\n\t\tЗадание #3")
    print(f"a*u + b*v = ({a})*({u}) + ({b})*({v}) = {a*u + b*v} = {gcd}")

    # Задание #4
    print("\n\t\tЗадание #4")
    print(f"({x})^({y}) mod {z} = {x**y % z}")


if __name__ == '__main__':
    main()

