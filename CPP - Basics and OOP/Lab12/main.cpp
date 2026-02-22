#include <iostream>

using namespace std;

template <class type>
class Stack
{
    int _size;
    int top;
    type *arr;

public:
    Stack() : _size(5), top(-1) { arr = new type[_size]; }
    Stack(int size) : _size(size), top(-1) { arr = new type[_size]; }
    ~Stack() { delete[] arr; }
    bool isEmpty() { return top == -1; }
    bool isFull() { return top == _size - 1; }
    Stack(const Stack<type> &st)
    {
        this->_size = st._size;
        this->top = st.top;

        arr = new type[_size];

        for (int i(0); i <= top; ++i)
            arr[i] = st.arr[i];
    }
    void push(type value)
    {
        if (isFull())
        {
            // cout << "Stack is Full\n";
            return;
        }
        arr[++top] = value;
    }
    bool pop(type &ret)
    {
        if (isEmpty())
        {
            // cout << "Stack is Empty\n";
            return false;
        }
        ret = arr[top--];
        return true;
    }
    void print()
    {
        for (int i(top); i > -1; --i)
            cout << arr[i] << ' ';

        cout << '\n';
    }
    Stack<type> &operator=(const Stack<type> &other)
    {
        if (this == &other)
            return *this;

        if (_size == other._size)
        {
            top = other.top;
            for (int i(0); i <= top; ++i)
                arr[i] = other.arr[i];
        }
        else
        {
            type *newArr(new type[other._size]);
            for (int i(0); i <= other.top; ++i)
                newArr[i] = other.arr[i];

            delete[] arr;
            arr = newArr;
            _size = other._size;
            top = other.top;
        }
        return *this;
    }
    Stack<type> operator+(const Stack<type> &other) const
    {
        Stack<type> result(_size + other._size);
        result.top = -1;

        for (int i(0); i <= top; ++i)
            result.arr[++result.top] = arr[i];

        for (int i(0); i <= other.top; ++i)
            result.arr[++result.top] = other.arr[i];

        return result;
    }
    friend ostream &operator<<(ostream &os, const Stack<type> &st)
    {
        for (int i(st.top); i >= 0; --i)
        {
            os << st.arr[i];
            if (i > 0)
                os << ' ';
        }
        return os;
    }
};

int main()
{
    Stack<int> s1(5);
    s1.push(1);
    s1.push(2);
    s1.push(3);

    Stack<int> s2(5);
    s2.push(10);
    s2.push(20);

    cout << "s1: " << s1 << '\n';
    cout << "s2: " << s2 << '\n';

    Stack<int> s3 = s1 + s2;
    cout << "s3: " << s3 << '\n';

    Stack<int> s4;
    s4 = s3;
    cout << "s4 (after = s3): " << s4 << '\n';

    Stack<int> s5 = s4;
    cout << "s5 (copy of s4): " << s5 << '\n';

    int val;
    if (s5.pop(val))
        cout << "popped from s5: " << val << '\n';

    cout << "s5 now: " << s5 << '\n';

    return 0;
}
