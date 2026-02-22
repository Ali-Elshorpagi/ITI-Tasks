#include <iostream>

using namespace std;

int _power(int b, int p)
{
    if (!p)
        return 1;
    return b * _power(b, p - 1);
}

int _factorial(int n)
{
    return ((!n || n == 1) ? 1 : (n * _factorial(n - 1)));
}

int fib(int n)
{
    if (!n)
        return 0;
    if (n < 2)
        return 1;
    return fib(n - 1) + fib(n - 2);
}

void fibonacci()
{
    int n;
    cout << "\n\tEnter a non-negative number: ";
    cin >> n;
    cout << "\n\tthe fibonacci of the number is: " << fib(n) << '\n';
}

void power()
{
    int x, y;
    cout << "\n\tEnter X (base): ";
    cin >> x;
    cout << "\n\tEnter Y (power): ";
    cin >> y;
    if (y > -1)
    {
        cout << "\n\t" << x << " ^ " << y << " = " << _power(x, y) << "\n\n";
        return;
    }
    cout << "\nThe Power should be non-negative number ...\n";
}

void factorial()
{
    int n;
    cout << "\n\tEnter a non-negative integer: ";
    cin >> n;
    if (n < 0)
    {
        cout << "\n\tError: Factorial is not defined for negative numbers!\n";
        return;
    }
    int result(_factorial(n));
    cout << "\n\tThe factorial of " << n << " is: " << result << "\n\n";
}

void TEST()
{
    cout << "\n\n\t << Power << \n\n";
    power();
    cout << "\n\t << Factorial << \n\n";
    factorial();
    cout << "\n\t << Fibonacci << \n\n";
    fibonacci();
}
