#include <iostream>

using namespace std;

class StaticStack
{
    int top;
    int arr[100];
    int size;
public:
    StaticStack()
    {
        top =-1;
        size = 100;
        cout << "\nInitialize Constructor Called\n";
    }
    void push(int value)
    {
        if(isFull())
        {
            cout << "Stack is Full\n";
            return;
        }
        arr[++top] = value;
    }
    int pop(int& ret)
    {
        if(isEmpty())
        {
            cout << "Stack is Empty\n";
            return 0;
        }
        ret = arr[top--];
        return 1;
    }
    bool isEmpty()
    {
        return top == -1;
    }
    bool isFull()
    {
        return top == size - 1;
    }
    void print()
    {
        for(int i(top); i > -1; --i)
            cout << arr[i] << ' ';

        cout << '\n';
    }
    ~StaticStack()
    {
        cout << "\nStatic Stack Destructor Called\n";
    }
};

void Test_StaticStack_value(StaticStack st)
{
    cout << "Called By Value Func: ";
    int val;
    if(st.pop(val))
        cout << val << ", ";
    if(st.pop(val))
        cout << val << ", ";
}
void Test_StaticStack_Reference(StaticStack& st)
{
    cout<<"Called By Address Func: ";
    int val;
    if(st.pop(val))
        cout << val << ", ";
    if(st.pop(val))
        cout << val << ", ";
}
void Test_StaticStack_pointer(StaticStack* st)
{
    cout<<"Called By Address Func: ";
    int val;
    if(st->pop(val))
        cout << val << ", ";
    if(st->pop(val))
        cout << val << ", ";
}
void Test_StaticStack()
{
    StaticStack st;
    st.push(8);
    st.push(9);
    st.push(7);
    Test_StaticStack_value(st);

    cout << "\nAfter By Value: : ";
    st.print();

    cout << "\n\n";
    Test_StaticStack_pointer(&st);
    cout << "\nAfter By Pointer: : ";
    st.print();

    cout << "\n\n";
    Test_StaticStack_Reference(st);
    cout <<"\nAfter By Reference: ";
    st.print();
}


class DynamicStack
{
private:
    int top;
    int* arr;
    int _size;
public:
    DynamicStack()
    {
        top = -1;
        _size = 100;
        arr = new int[_size];
        cout << "\nInitialize Constructor Called\n";
    }

    DynamicStack(int size)
    {
        this->_size = size;
        top = -1;
        arr = new int[_size];
        cout << "\nParamaterize Constructor Called\n";
    }

    DynamicStack(DynamicStack& st)
    {
        this->_size = st._size;
        this->top = st.top;

        arr = new int[_size];

        for(int i(0); i <= top; ++i)
            arr[i] = st.arr[i];

        cout << "\nCopy Constructor Called\n";
    }

    void push(int value)
    {
        if(isFull())
        {
            cout << "Stack is Full\n";
            return;
        }
        arr[++top] = value;
    }

    int pop(int& ret)
    {
        if(isEmpty())
        {
            cout << "Stack is Empty\n";
            return 0;
        }
        ret = arr[top--];
        return 1;
    }

    bool isEmpty()
    {
        return top == -1;
    }

    bool isFull()
    {
        return top == _size - 1;
    }

    void print()
    {
        for(int i(top) ; i > -1 ; --i)
            cout << arr[i] << ' ';

        cout << '\n';
    }

    ~DynamicStack()
    {
        delete[] arr;
        cout << "\nDynamic Stack Destructor Called\n";
    }
};


void Test_DynamicStack_Value(DynamicStack st)
{
    cout << "Called By Value Func: ";
    int val;
    if(st.pop(val))
        cout << val << ", ";
    if(st.pop(val))
        cout << val << ", ";
}
void Test_DynamicStack_Reference(DynamicStack& st)
{
    cout<<"Called By Address Func: ";
    int val;
    if(st.pop(val))
        cout << val << ", ";
    if(st.pop(val))
        cout << val << ", ";
}
void Test_DynamicStack_Pointer(DynamicStack* st)
{
    cout<<"Called By Address Func: ";
    int val;
    if(st->pop(val))
        cout << val << ", ";
    if(st->pop(val))
        cout << val << ", ";
}
void Test_DynamicStack()
{
    DynamicStack st;
    st.push(8);
    st.push(9);
    st.push(7);

    Test_DynamicStack_Value(st);
    cout<<"\nAfter By Value: ";
    st.print();


    cout << "\n\n";
    Test_DynamicStack_Pointer(&st);
    cout<<"\nAfter By Pointer: ";
    st.print();


    cout << "\n\n";
    Test_DynamicStack_Reference(st);
    cout << "\nAfter By Reference: ";
    st.print();


}

int main()
{
    Test_StaticStack();
    cout << "\n-------------------------------------\n\n";
    Test_DynamicStack();
    return 0;
}
