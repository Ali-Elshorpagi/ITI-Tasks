#ifndef CIRCULAR_QUEUE
#define CIRCULAR_QUEUE

#include <iostream>
#include <cassert>

#define edl '\n'

template <class type>
class Circular_Queue
{
    int front{};
    int rear{};
    int size{};
    int added_elements{};
    type *arr{};
    int next(int pos) { return (pos + 1) % size; }

public:
    Circular_Queue(int sz) : size(sz) { arr = new type[size]; }
    ~Circular_Queue()
    {
        delete[] arr;
        arr = nullptr;
        size = added_elements = 0;
    }
    bool is_full() { return added_elements == size; }
    bool is_empty() { return added_elements == 0; }
    const int get_size() { return size; }
    void enqueue(type val)
    {
        assert(!is_full());
        arr[rear] = val;
        rear = next(rear);
        ++added_elements;
    }
    bool insert_after(const type &target, const type &val)
    {
        if (is_empty())
            return false;

        int idx(-1);
        for (int cur(front), step(0); step < added_elements; ++step, cur = next(cur))
        {
            if (arr[cur] == target)
            {
                idx = cur;
                break;
            }
        }
        if (idx == -1 || is_full())
            return false;

        int insert_pos(next(idx)), end(rear), pos(rear);

        for (int count(added_elements); count > 0; --count)
        {
            int prev((pos - 1 + size) % size);
            arr[pos] = arr[prev];
            pos = prev;
            if (pos == insert_pos)
                break;
        }
        arr[insert_pos] = val;
        rear = next(rear);
        ++added_elements;
        return true;
    }

    bool insert_before(const type &target, const type &val)
    {
        if (is_empty())
            return false;

        int idx(-1);
        for (int cur(front), step(0); step < added_elements; ++step, cur = next(cur))
        {
            if (arr[cur] == target)
            {
                idx = cur;
                break;
            }
        }
        if (idx == -1 || is_full())
            return false;

        int pos(rear);

        for (int count(added_elements); count > 0; --count)
        {
            int prev((pos - 1 + size) % size);
            arr[pos] = arr[prev];
            pos = prev;
            if (pos == idx)
                break;
        }
        arr[idx] = val;
        rear = next(rear);
        ++added_elements;
        return true;
    }
    type dequeue()
    {
        assert(!is_empty());
        type value(arr[front]);
        front = next(front);
        --added_elements;
        return value;
    }
    void print()
    {
        std::cout << "Front " << front << " - Rear " << rear << '\t';
        if (is_full())
            std::cout << "Full" << edl;
        else if (is_empty())
        {
            std::cout << "Empty" << edl << edl;
            return;
        }
        std::cout << edl;
        for (int cur(front), step(0); step < added_elements; ++step, cur = next(cur))
            std::cout << arr[cur] << ' ';
        std::cout << edl << edl;
    }
};
#endif