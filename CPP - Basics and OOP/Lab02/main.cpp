#include <iostream>
#include <windows.h>

using namespace std;

void gotoxy(int column, int line)
{
    COORD coord;
    coord.X = column;
    coord.Y = line;
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

int main()
{
    int choice(0);
    while (choice != 10)
    {
        system("cls");
        cout << "\n\t[1]. Check if the number is prime or not"
             << "\n\t[2]. Get your Grade upon the degree"
             << "\n\t[3]. Sum of 5 numbers"
             << "\n\t[4]. Count No. Words and Characters"
             << "\n\t[5]. Get the Factorial of a number"
             << "\n\t[6]. Enter X, Y, and Get X ^ Y"
             << "\n\t[7]. Get the reverse of a number"
             << "\n\t[8]. Get the binary representation of a number"
             << "\n\t[9]. Get the magic box by entering an odd number"
             << "\n\t[10]. Exit"
             << "\n\n\tEnter Your Choice: ";
        cin >> choice;
        switch (choice)
        {
        case 1:
        {
            int num1;
            bool is_prime(true);
            cout << "\n\tEnter a number: ";
            cin >> num1;
            if (num1 < 2)
                is_prime = false;
            else
            {
                is_prime = true;
                for (int i(2); i * i <= num1; ++i)
                {
                    if (num1 % i == 0)
                    {
                        is_prime = false;
                        break;
                    }
                }
                if (is_prime)
                    cout << "\n\t" << num1 << " is a prime number.\n\n";
                else
                    cout << "\n\t" << num1 << " is not a prime number.\n\n";
            }
            system("pause");
            break;
        }
        case 2:
        {
            int degree(0);
            cout << "\n\tEnter your degree: ";
            cin >> degree;
            char grade;
            if (degree > 89)
                grade = 'A';
            else if (degree > 79)
                grade = 'B';
            else if (degree > 69)
                grade = 'C';
            else if (degree > 59)
                grade = 'D';
            else
                grade = 'F';
            cout << "\n\tYour grade is: " << grade << "\n\n";
            system("pause");
            break;
        }
        case 3:
        {
            int sum(0), number(0);
            cout << "\n\tEnter 5 numbers to sum:\n";
            for (int i(1); i < 6; ++i)
            {
                cout << "\tNumber " << i << ": ";
                cin >> number;
                sum += number;
            }
            cout << "\n\tThe sum of the 5 numbers is: " << sum << "\n\n";
            system("pause");
            break;
        }
        case 4:
        {
            int char_count(0), word_count(0), size(0);
            string text;
            cout << "\n\tEnter a line of text: ";
            cin.ignore();
            getline(cin, text);
            size = text.length();

            bool is_word(false);
            for (int i(0); i < size; ++i)
            {
                if (text[i] != 32)
                {
                    ++char_count;

                    if (!is_word)
                        is_word = true, ++word_count;
                }
                else
                    is_word = false;
            }
            cout << "\n\tNumber of words: "
                 << word_count
                 << "\n\tNumber of characters (excluding spaces): "
                 << char_count
                 << "\n\n";

            system("pause");
            break;
        }
        case 5:
        {
            int num2;
            cout << "\n\tEnter a number: ";
            cin >> num2;
            if (num2 < 0)
                cout << "\n\tFactorial is undefined for negative numbers.\n\n";
            else
            {
                long long fact(1);
                for (long long i(2); i <= num2; ++i)
                    fact *= i;
                cout << "\n\t" << num2 << "! = " << fact << "\n\n";
            }
            system("pause");
            break;
        }
        case 6:
        {
            // 5 = (101)
            // 5 = 4 + 1
            // 3^5 = 3^(4 + 1) = 3^4 × 3^1 = 243

            int x, y;
            cout << "\n\tEnter X (base): ";
            cin >> x;
            cout << "\n\tEnter Y (power): ";
            cin >> y;
            if (y > -1)
            {
                int power(y);
                long long result(1), base(x);
                while (power)
                {
                    if (power & 1)
                        result *= base;
                    power >>= 1;
                    base *= base;
                }
                cout << "\n\t" << x << " ^ " << y << " = " << result << "\n\n";
            }

            system("pause");
            break;
        }
        case 7:
        {
            int num3, reversed_num(0);
            cout << "\n\tEnter a number: ";
            cin >> num3;
            bool is_negative(num3 < 0);
            num3 = (is_negative ? -num3 : num3);
            while (num3 > 0)
            {
                reversed_num = reversed_num * 10 + (num3 % 10);
                num3 /= 10;
            }

            if (is_negative)
                cout << "\n\tReversed number: -" << reversed_num << "\n\n";
            else
                cout << "\n\tReversed number: " << reversed_num << "\n\n";

            system("pause");
            break;
        }
        case 8:
        {
            int num4;
            cout << "\n\tEnter a non-negative number: ";
            cin >> num4;
            if (!num4)
                cout << "\n\tBinary: 0\n\n";
            else if (num4 < 0)
                cout << "\n\tPlease enter a positive number.\n\n";
            else
            {
                string bits("");
                while (num4 > 0)
                {
                    bits += (num4 % 2) ? '1' : '0';
                    num4 >>= 1;
                }

                string binary("");
                for (int i(bits.length() - 1); i > -1; --i)
                    binary += bits[i];

                cout << "\n\tBinary: " << binary << "\n\n";
            }

            system("pause");
            break;
        }
        case 9:
        {
            int num5;
            cout << "\n\tEnter an odd positive number: ";
            cin >> num5;

            if (num5 < 1 || !(num5 & 1))
                cout << "\n\tPlease enter a positive odd number.\n\n";
            else
            {
                system("cls");
                cout << "\n\tMagic Box of size " << num5 << "x" << num5 << ":\n\n";

                int x(num5 >> 1), // start column (middle)
                    y(0),         // start row (top)
                    start_x(10),
                    start_y(3),
                    space_x(4), // space between columns
                    space_y(2), // space between rows
                    prev_num(1);

                gotoxy(start_x + x * space_x, start_y + y * space_y);
                cout << prev_num;

                for (int num(2); num <= num5 * num5; ++num)
                {
                    if (prev_num % num5 == 0)
                        y = (y + 1) % num5;
                    else
                        y = (y - 1 + num5) % num5, x = (x - 1 + num5) % num5;

                    gotoxy(start_x + x * space_x, start_y + y * space_y);
                    cout << num;

                    prev_num = num;
                }

                gotoxy(0, start_y + num5 * space_y + 2);
            }

            system("pause");
            break;
        }
        case 10:
            cout << "\n\tExiting the application. Goodbye!\n";
            break;
        default:
            cout << "\n\tInvalid choice! Please try again from 1 to 10.\n";
            system("pause");
            break;
        }
    }

    return 0;
}
