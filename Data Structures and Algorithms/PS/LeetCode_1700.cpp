#include <bits/stdc++.h>
// #include "Elshorpagi.h"

using namespace std;

typedef long long ll;
typedef pair<int, int> pii;
typedef vector<int> vi;
typedef vector<vi> vvi;
typedef vector<ll> vll;
typedef vector<vll> vvll;
typedef vector<pii> vpii;
typedef vector<char> vc;

#define _CRT_SECURE_NO_DEPRECATE
#define __elshorpagi__ (ios_base::sync_with_stdio(false), cin.tie(NULL))
#define all(v) ((v).begin()), ((v).end())
#define sz(v) ((int)((v).size()))
#define cl(v) ((v).clear())
#define edl '\n'
#define fr(i, x, n) for (int i(x); i < n; ++i)
#define fl(i, x, n) for (int i(x); i > n; --i)
#define fc(it, v) for (auto &(it) : (v))
#define sq(x) (x) * (x)
#define yes cout << "YES\n"
#define no cout << "NO\n"

struct TreeNode
{
    int val;
    TreeNode *left;
    TreeNode *right;
    TreeNode() : val(0), left(nullptr), right(nullptr) {}
    TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
    TreeNode(int x, TreeNode *left, TreeNode *right) : val(x), left(left), right(right) {}
};

struct ListNode
{
    int val;
    ListNode *next;
    ListNode() : val(0), next(nullptr) {}
    ListNode(int x) : val(x), next(nullptr) {}
    ListNode(int x, ListNode *next) : val(x), next(next) {}
};

class Solution
{
public:
    Solution() { __elshorpagi__; }
    int countStudents(vector<int> &students, vector<int> &sandwiches)
    {
        int size(sz(students)), tries(0);
        queue<int> qu;
        fr(i, 0, size) { qu.push(students[i]); }
        stack<int> st;
        fl(i, size - 1, -1) { st.push(sandwiches[i]); }

        while (!qu.empty() && !st.empty())
        {
            if (qu.front() == st.top())
                qu.pop(), st.pop(), tries = 0;
            else
            {
                int element(qu.front());
                qu.pop(), qu.emplace(element);
                ++tries;
            }
            if (tries == sz(qu))
                break;
        }
        return qu.size();
    }
    void TEST() {}
};

int main()
{
    Solution sol;
    freopen("test/input.txt", "r", stdin);
    freopen("test/output.txt", "w", stdout);
    int tc(1);
    cin >> tc;
    while (tc--)
        cout << "Case #" << tc + 1 << edl, sol.TEST();
    cout << edl << "DONE" << edl;
    return (0);
}