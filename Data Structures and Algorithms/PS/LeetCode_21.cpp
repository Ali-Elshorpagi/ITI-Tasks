#include <bits/stdc++.h>

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
;

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
    ListNode *mergeTwoLists__(ListNode *list1, ListNode *list2)
    {
        // 2 4 5 - 4 5 6
        ListNode *dummy(new ListNode(-1));
        ListNode *cur(dummy);
        while (list1 && list2)
        {
            if (list1->val > list2->val)
                cur->next = list2, list2 = list2->next;
            else
                cur->next = list1, list1 = list1->next;
            cur = cur->next;
        }
        cur->next = (list1 ? list1 : list2);
        return dummy->next;
    }
    ListNode *mergeTwoLists(ListNode *list1, ListNode *list2)
    {
        if (!list1)
            return list2;
        if (!list2)
            return list1;

        ListNode *ans;
        if (list1->val < list2->val)
            ans = list1, ans->next = mergeTwoLists(list1->next, list2);
        else
            ans = list2, ans->next = mergeTwoLists(list1, list2->next);

        return ans;
    }
    void TEST() {}
};

int main()
{
    Solution sol;
    // freopen("test/input.txt", "r", stdin);
    // freopen("test/output.txt", "w", stdout);
    int tc(1);
    // cin >> tc;
    while (tc--)
        cout << "Case #" << tc + 1 << edl, sol.TEST();
    cout << edl << "DONE" << edl;
    return (0);
}