#include <iostream>
#include <string.h>

#define LEN 10
#define MAX 100
#define MAXLEN 205
#define ROWS 3
#define COLS 4

using namespace std;

int main()
{
    int choice(0);
    while (choice != 6)
    {
        system("cls");
        cout << "\n\t[1]. One Dimention array"
             << "\n\t[2]. Tow Dimentions array"
             << "\n\t[3]. Concat two strings in another variable"
             << "\n\t[4]. Put 3 names of students and print them"
             << "\n\t[5]. Put string and print it with converting small letters to capital letters and vice versa"
             << "\n\t[6]. Exit"
             << "\n\n\tEnter Your Choice: ";
        cin >> choice;
        switch (choice)
        {
        case 1:
        {
            int arr[LEN];
            int cho(0);
            while (cho != 8)
            {
                system("cls");
                cout << "\n\t[1]. Insert the array data"
                     << "\n\t[2]. Print items of the array"
                     << "\n\t[3]. Get the Maximum item in the array"
                     << "\n\t[4]. Get the Minimum item in the array"
                     << "\n\t[5]. Get the sum of array items"
                     << "\n\t[6]. Search for an item in the array"
                     << "\n\t[7]. Sort the array using Bubble Sort"
                     << "\n\t[8]. Back to Main Menu"
                     << "\n\n\tEnter Your Choice: ";
                cin >> cho;
                switch (cho)
                {
                case 1:
                {
                    for (int i(0); i < LEN; ++i)
                    {
                        cout << "\tEnter item " << i + 1 << ": ";
                        cin >> arr[i];
                    }
                    system("pause");
                    break;
                }
                case 2:
                {
                    for (int i(0); i < LEN; ++i)
                        cout << "\n\tItem " << i + 1 << ": " << arr[i];
                    cout << '\n';
                    system("pause");
                    break;
                }
                case 3:
                {
                    int mx(arr[0]);
                    for (int i(0); i < LEN; ++i)
                    {
                        if (arr[i] > mx)
                            mx = arr[i];
                    }
                    cout << "\n\tThe Maximum item in the array is: " << mx << "\n";
                    system("pause");
                    break;
                }
                case 4:
                {
                    int mn(arr[0]);
                    for (int i(0); i < LEN; ++i)
                    {
                        if (arr[i] < mn)
                            mn = arr[i];
                    }
                    cout << "\n\tThe Minimum item in the array is: " << mn << "\n";
                    system("pause");
                    break;
                }
                case 5:
                {
                    int sum(0);
                    for (int i(0); i < LEN; ++i)
                        sum += arr[i];
                    cout << "\n\tThe sum of array items is: " << sum << "\n";
                    system("pause");
                    break;
                }
                case 6:
                {
                    int item, index(-1);
                    cout << "\n\tEnter the item to search for: ";
                    cin >> item;
                    for (int i(0); i < LEN; ++i)
                    {
                        if (arr[i] == item)
                        {
                            index = i;
                            break;
                        }
                    }
                    cout << "\n\t";
                    if (index != -1)
                        cout << "Item found at index " << index << ".\n";
                    else
                        cout << "Item not found in the array.\n";
                    system("pause");
                    break;
                }
                case 7:
                {
                    for (int i(0); i < LEN - 1; ++i)
                    {
                        bool swapped(false);
                        for (int j(0); j < LEN - i - 1; ++j)
                        {
                            if (arr[j] > arr[j + 1])
                            {
                                int temp(arr[j]);
                                arr[j] = arr[j + 1];
                                arr[j + 1] = temp;
                                swapped = true;
                            }
                        }
                        if (!swapped) // array is sorted
                            break;
                    }
                    cout << "\n\tArray sorted using Bubble Sort.\n";
                    for (int i(0); i < LEN; ++i)
                        cout << "\n\tItem " << i + 1 << ": " << arr[i];
                    system("pause");
                    break;
                }
                case 8:
                    cout << "\n\tReturning to Main Menu...\n";
                    break;
                default:
                    cout << "\n\tInvalid choice! Please try again from 1 to 8.\n";
                    system("pause");
                    break;
                }
            }
            break;
        }
        case 2:
        {

            int matrix[ROWS][COLS];
            int cho(0);
            while (cho != 5)
            {
                system("cls");
                cout << "\n\t[1]. Insert the 2D array data"
                     << "\n\t[2]. Print items of the 2D array"
                     << "\n\t[3]. Get the sum of each row in the 2D array"
                     << "\n\t[4]. Get the average of each column in the 2D array"
                     << "\n\t[5]. Back to Main Menu"
                     << "\n\n\tEnter Your Choice: ";
                cin >> cho;
                switch (cho)
                {
                case 1:
                {
                    for (int i(0); i < ROWS; ++i)
                    {
                        for (int j(0); j < COLS; ++j)
                        {
                            cout << "\tEnter item [" << i << "][" << j << "]: ";
                            cin >> matrix[i][j];
                        }
                    }
                    system("pause");
                    break;
                }
                case 2:
                {
                    for (int i(0); i < ROWS; ++i)
                    {
                        cout << '\t';
                        for (int j(0); j < COLS; ++j)
                            cout << matrix[i][j] << ' ';
                        cout << '\n';
                    }
                    system("pause");
                    break;
                }
                case 3:
                {
                    for (int i(0); i < ROWS; ++i)
                    {
                        int rowSum(0);
                        for (int j(0); j < COLS; ++j)
                            rowSum += matrix[i][j];
                        cout << "\n\tSum of row " << i + 1 << " is: " << rowSum << "\n";
                    }
                    system("pause");
                    break;
                }
                case 4:
                {
                    for (int j(0); j < COLS; ++j)
                    {
                        double colSum(0);
                        for (int i(0); i < ROWS; ++i)
                            colSum += matrix[i][j];
                        cout << "\n\tAverage of column " << j + 1 << " is: " << colSum / ROWS << "\n";
                    }
                    system("pause");
                    break;
                }
                case 5:
                    cout << "\n\tReturning to Main Menu...\n";
                    break;
                default:
                    cout << "\n\tInvalid choice! Please try again from 1 to 5.\n";
                    system("pause");
                    break;
                }
            }
            break;
        }
        case 3:
        {
            char str1[MAX], str2[MAX], concatStr[MAXLEN];
            cout << "\n\tEnter the first string: ";
            cin >> str1;
            cout << "\n\tEnter the second string: ";
            cin >> str2;
            strcpy(concatStr, str1);
            strcat(concatStr, " ");
            strcat(concatStr, str2);
            cout << "\n\tConcatenated string: " << concatStr << '\n';
            system("pause");
            break;
        }
        case 4:
        {
            char students[3][MAX];
            cout << '\n';
            for (int i(0); i < 3; ++i)
            {
                cout << "\tEnter name of student " << i + 1 << ": ";
                cin >> students[i];
            }
            cout << "\n\tThe names of the students are:\n";
            for (int i(0); i < 3; ++i)
                cout << "\t" << i + 1 << ". " << students[i] << '\n';
            cout << '\n';
            system("pause");
            break;
        }
        case 5:
        {
            char str[MAX];
            cout << "\n\tEnter a string: ";
            cin >> str;
            for (int i(0); str[i] != '\0'; ++i)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                    str[i] = str[i] - 32;
                else if (str[i] >= 'A' && str[i] <= 'Z')
                    str[i] = str[i] + 32;
            }
            cout << "\n\tConverted string: " << str << '\n';
            cin.ignore();
            system("pause");
            break;
        }
        case 6:
            cout << "\n\tExiting the application. Goodbye!\n";
            break;
        default:
            cout << "\n\tInvalid choice! Please try again from 1 to 6.\n";
            system("pause");
            break;
        }
    }

    return 0;
}
