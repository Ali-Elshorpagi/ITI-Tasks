#include <iostream>

using namespace std;

template <class type>
void _Swap(type &first, type &second)
{
    type tmp(first);
    first = second;
    second = tmp;
}

int _gcd_iterative(int A, int B)
{
    if (A < 0)
        A = -A;
    if (B < 0)
        B = -B;
    if (A < B)
        _Swap(A, B);

    while (A && B)
    {
        int R(A % B);
        A = B;
        B = R;
    }
    return A;
}

class Fraction
{
    int _numerator, _denominator;
    static int counter;

    void simplification()
    {
        if (_denominator < 0)
            _denominator = -_denominator, _numerator = -_numerator;

        int g(_gcd_iterative(_numerator, _denominator));
        if (g != 0)
            _numerator /= g, _denominator /= g;
    }

public:
    Fraction() : _numerator(1), _denominator(1) { ++counter; }
    Fraction(int numerator, int denominator) : _numerator(numerator), _denominator(denominator == 0 ? 1 : denominator) { simplification(), ++counter; }
    Fraction(const Fraction &other) { _numerator = other._numerator, _denominator = other._denominator, ++counter; }
    ~Fraction() { --counter; }
    static int get_counter() { return counter; }
    Fraction &operator=(const Fraction &other)
    {
        if (this != &other)
            _numerator = other._numerator, _denominator = other._denominator;
        return *this;
    }
    // a/b + c/d = (a*d + c*b) / (b*d)
    Fraction operator+(const Fraction &other) const
    {
        Fraction tmp;
        tmp._numerator = (_numerator * other._denominator) + (other._numerator * _denominator);
        tmp._denominator = _denominator * other._denominator;
        tmp.simplification();
        return tmp;
    }
    Fraction operator+(int value) const // Fraction + int
    {
        Fraction tmp;
        tmp._numerator = _numerator + value * _denominator;
        tmp._denominator = _denominator;
        tmp.simplification();
        return tmp;
    }
    friend Fraction operator+(int value, const Fraction &other) // int + Fraction
    {
        Fraction tmp;
        tmp = other + value;
        return tmp;
    }
    bool operator==(const Fraction &other) const { return _numerator == other._numerator && _denominator == other._denominator; }
    bool operator!=(const Fraction &other) const { return !(*this == other); }
    // a/b , c/d => a*d ? c*b
    bool operator<(const Fraction &f) const { return _numerator * f._denominator < f._numerator * _denominator; }
    bool operator>(const Fraction &f) const { return _numerator * f._denominator > f._numerator * _denominator; }
    Fraction &operator++() // prefix
    {
        _numerator += _denominator;
        simplification();
        return *this;
    }
    Fraction operator++(int) // postfix
    {
        Fraction tmp(*this);
        ++(*this);
        return tmp;
    }
    friend ostream &operator<<(ostream &os, const Fraction &f)
    {
        os << f._numerator << "/" << f._denominator;
        return os;
    }
    // explicit operator float() const
    // {
    //     return (float)_numerator / (float)_denominator;
    // }
    operator float() const
    {
        return (float)_numerator / (float)_denominator;
    }
};

int Fraction::counter = 0;

int main()
{
    cout << "Counter : " << Fraction::get_counter() << '\n';
    Fraction a(1, 2);
    cout << "Counter : " << Fraction::get_counter() << '\n';
    Fraction b(3, 4);
    cout << "Counter : " << Fraction::get_counter() << '\n';

    cout << "a = " << a << '\n';
    cout << "b = " << b << '\n';

    Fraction c = a + b;
    cout << "a + b = " << c << '\n';

    cout << "a == b ? " << (a == b) << '\n';
    cout << "a != b ? " << (a != b) << '\n';
    cout << "a < b ? " << (a < b) << '\n';
    cout << "a > b ? " << (a > b) << '\n';

    Fraction d = b++;
    cout << "b++ gives old value = " << d << '\n';
    cout << "b has new value = " << b << '\n';

    Fraction z = a + 2;
    cout << "a + 2 = " << z << '\n';

    Fraction e = 2 + a;
    cout << "2 + a = " << e << '\n';

    cout << "++a = " << ++a << '\n';
    cout << "a++ (old) = " << a++ << '\n';
    cout << "now a = " << a << '\n';

    float af((float)a);
    cout << "float(a) = " << af << '\n';

    float bf = a;
    cout << "float(a) = " << bf << '\n';

    cout << "Counter : " << Fraction::get_counter() << '\n';
    return 0;
}
