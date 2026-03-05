#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

typedef vector<int> vi;

#define _CRT_SECURE_NO_DEPRECATE
#define sz(v) ((int)((v).size()))
#define edl '\n'

/*
 * # Complexities
 *
 * Best-Case Time     O(1)
 * Worst-Case Time    O(log(N))
 * Worst-Case Space   O(1), but in recursive implementation will be O(log(N))
 */

class Algorithm
{
public:
    int Binary_Search(vi &arr, int value)
    {
        int left(0), right(sz(arr) - 1);
        while (left <= right)
        {
            // we can use ==> mid = left + (right - left + 1) / 2; it's depends on problem
            int mid(left + ((right - left) >> 1));
            if (arr[mid] == value)
                return mid;
            else if (arr[mid] > value)
                right = mid - 1;
            else
                left = mid + 1;
        }
        return -1;
    }

    int Binary_Search_Recursive(vi &arr, int left, int right, int value)
    {
        if (right >= left)
        {
            int mid(left + ((right - left) >> 1));
            if (arr[mid] == value)
                return mid;
            if (arr[mid] > value)
                return Binary_Search_Recursive(arr, left, mid - 1, value);
            return Binary_Search_Recursive(arr, mid + 1, right, value);
        }
        return -1;
    }

    void TEST()
    {
        int n, target;
        cout << "Enter The size of the array: ";
        cin >> n;
        vi arr(n);
        cout << "Enter the elements of the array (sorted): ";
        for (auto &it : arr)
            cin >> it;
        cout << "Enter the targe u want search for: ";
        cin >> target;
        cout << edl << "Iterative: " << Binary_Search(arr, target) << edl;
        cout << "Recursive: " << Binary_Search_Recursive(arr, 0, n - 1, target) << edl;
    }
};

int main()
{
    Algorithm algo;
    algo.TEST();
    cout << edl << "DONE" << edl;

    return (0);
}