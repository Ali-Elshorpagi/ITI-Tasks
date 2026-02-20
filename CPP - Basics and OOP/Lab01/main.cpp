#include <iostream>

using namespace std;

int main()
{
    int choice(0);
    while (choice != 5)
    {
        system("cls");
        cout << "\n\t    Start Application"
             << "\n\t-------------------------"
             << "\n\tPowered By Ali Elshorpagi"
             << "\n\t-------------------------\n\n";
        cout << "\n\t[1]. Get the ASCII code of a char"
             << "\n\t[2]. Get the char from its integer"
             << "\n\t[3]. Arithmetic operation on two numbers"
             << "\n\t[4]. Convert Small char to capital char or vice versa"
             << "\n\t[5]. Exit"
             << "\n\n\tEnter Your Choice: ";
        cin >> choice;
        switch (choice)
        {
        case 1:
        {
            char character;
            cout << "\n\tEnter a character: ";
            cin >> character;
            cout << "\n\tThe ASCII code of the character is: "
                 << int(character)
                 << '\n';
            system("pause");
            break;
        }
        case 2:
        {
            int ascii_Code;
            cout << "\n\tEnter an ASCII code (integer): ";
            cin >> ascii_Code;
            cout << "\n\tThe character of the ASCII code is: \'"
                 << char(ascii_Code)
                 << "\'\n";
            system("pause");
            break;
        }
        case 3:
        {
            double num1, num2;
            char operation;
            cout << "\n\tEnter two numbers and an arithmetic operation (+, -, *, /) respectively (e.g. 14 5 +) : ";
            cin >> num1 >> num2 >> operation;
            double result;
            switch (operation)
            {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            case '*':
                result = num1 * num2;
                break;
            case '/':
            {
                if (num2 != 0)
                    result = num1 / num2;
                else
                {
                    cout << "\n\tError: Division by zero!\n";
                    system("pause");
                    continue;
                }
            }
            break;
            default:
                cout << "\n\tInvalid operation!\n";
                system("pause");
                continue;
            }
            cout << "\n\tThe result is: "
                 << result
                 << '\n';
            system("pause");
            break;
        }
        case 4:
        {
            char character;
            cout << "\n\tEnter a character: ";
            cin >> character;
            if (character >= 'a' && character <= 'z')
                character = character - 32;
            else if (character >= 'A' && character <= 'Z')
                character = character + 32;
            else
            {
                cout << "\n\tError: Not an alphabetic character!\n";
                system("pause");
                break;
            }
            cout << "\n\tThe converted character is: '"
                 << character
                 << "'\n";
            system("pause");
            break;
        }
        case 5:
            cout << "\n\tExiting the application. Goodbye!\n";
            break;
        default:
            cout << "\n\tInvalid choice! Please try again from 1 to 5.\n";
            system("pause");
            break;
        }
    }

    return 0;
}
